# Kompare
Machine Learning Screenshot Compare

You'll need chromedriver to get the screenshots. I plan to add support for other browsers and selenium grid support. I also plan to allow training from directories with screenshots.

As of now the diff threshold is hard coded but I plan to add a confidence. You train it with possible page layouts and it will return the closest image. If the confidence is too low the compare failed. 