# GigE-Cam-Simulator

This is a GigE Camera Simulator written in C#

## Features:
- Discover: after starting this Camera should by answering discovery requests
- Software trigger: acquire and provide images for software trigger requests

## How it works:
The simulator is a state machine that provides access to a simulated camera memory. A GigE Vision Client can read and write this memory/registers to control the fake camera. 
When the simulator starts it uses the "data/memory.xml" file to initial write default values to the memory (e.g. "Manufacturer_name" or "image-width"). 
Also, the camera description file "data/camera.xml" is written to the memory on startup that contains the feature description of the camera.

When the client writes to register 0x30c an image is sent to the client.

## To config your simulator camera you need to:
- edit "data/memory.xml" to setup memory values
- edit "data/camera.xml" if you want to change camera features
- edit "Program.cs" to add logic to your camera and provide image-files

## Specification
see:
https://www.visiononline.org/userAssets/aiaUploads/File/GigE_Vision_Specification_2-0-03.pdf



## Inspired by
- [Aravis](https://github.com/AravisProject/aravis)
- [tualo / gigecamera-simulator](https://github.com/tualo/gigecamera-simulator)

see also:
- https://github.com/Strongc/VirtualGEVCam
