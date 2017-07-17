using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace kompare
{
    class screenshot
    {
        Dictionary<int, pixelMinMax> set = new Dictionary<int, pixelMinMax>();
        IWebDriver driver = null;
        public screenshot()
        {
            driver = new ChromeDriver(@"k:\chromedriver");
        }

        public void closeChrome()
        {
            driver.Close();
        }
        ~screenshot()
        {
            driver.Quit();
        }
        public Image train()
        {
            String[] movies = loadCsv();
            Image b = new Bitmap(1, 2);
            foreach (String m in movies)
            {
                createHt(screenshotToBitmap(takeScreenshot(m)));
            }


            return b;
        }

        public Screenshot takeScreenshot(string q)
        {
            driver.Navigate().GoToUrl("https://www.google.com/search?q=" + q);
            return ((ITakesScreenshot)driver).GetScreenshot();
        }

        public Bitmap screenshotToBitmap(Screenshot ss)
        {
            Byte[] bitmapData = Convert.FromBase64String(ss.AsBase64EncodedString);
            System.IO.MemoryStream streamBitmap = new System.IO.MemoryStream(bitmapData);
            return new Bitmap((Bitmap)Image.FromStream(streamBitmap));
        }

        public void createHt(Image ss)
        {

            int width = ss.Width;
            int height = ss.Height;
            int count = 0;
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++) {
                    int pix = ((Bitmap)ss).GetPixel(j, i).ToArgb();
                    if (!set.ContainsKey(count))
                    {
                        set.Add(count, new pixelMinMax(pix, count++));
                    }
                    else
                    {
                        set[count].edit(pix);
                    }

                }
            }
        }


        public void createfile()
        {
            String s = "";
            StringBuilder sb = new StringBuilder(s);
            for (int i = 0; i < set.Count; i++)
            {
                sb.Append(set[i].toString() + Environment.NewLine);
            }
            saveFile(sb.ToString());
        }

        public void saveFile(String str)
        {
            string path = @"MyTest.txt";
            if (!File.Exists(path))
                File.WriteAllText(path, "");
            File.WriteAllText(path, str);
        }

        public string[] loadCsv()
        {
            string path = @"movies.csv";
            String csv = File.ReadAllText(path);
            return csv.Split(',');
        }


        public void loadData()
        {
            string path = @"MyTest.txt";
            String[] csv = File.ReadAllLines(path);
            foreach (String s in csv)
            {
                String[] exp = s.Split(',');
                if (exp.Length == 3)
                {
                    set.Add(Int32.Parse(exp[0]), new pixelMinMax(Int32.Parse(exp[1]), Int32.Parse(exp[2]), Int32.Parse(exp[0])));
                }
            }
        }

        public Boolean compare(Image im)
        {
            int width = im.Width;
            int height = im.Height;
            int count = 0;
            int offCount = 0;
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    int pix = ((Bitmap)im).GetPixel(j, i).ToArgb();
                    if (!set[count++].compare(pix))
                        offCount++;
                    if(offCount > ((im.Height * im.Width) * .02 ))
                        return false;
                }
            }
            return true;
        }
    }
}
