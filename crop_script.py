from PIL import Image
import os


index = 0

for file in os.listdir(r'Defect_images')[:10]:
    # Opens a image in RGB mode
    im = Image.open(f"Defect_images/{file}")

    # Size of the image in pixels (size of orginal image)
    # (This is not mandatory)
    width, height = im.size

    # Setting the points for cropped image
    left = 5
    top = height / 4
    right = 164
    bottom = 3 * height / 4

    for x in range(0, 4046, 512):
        # Cropped image of above dimension
        # (It will not change orginal image)
        im1 = im.crop((x, 0, x+512, 256))

        # Shows the image in image viewer
        im1.save(f'Defect_images_2/{index}.png')
        index += 1

