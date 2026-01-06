This project was created after being unhappy that the instructions on how to extract symbols from a Dart / Flutter Android app was to basically create a modified APK and run that on one's phone.

At the moment it is a work-in-progress, only parsing the image and requiring running under a debugger (e.g. in Visual Studio) to view the parsed objects.

I have tested this with a couple of Dart AOT files (one compiled with an X64 Dart 3.7.2 build, and one from an Android app).  I intend to add support for Dart versions earlier than 3.7.0 and to provide options to extract symbols and class layouts.

Once I am happy with this, I might see if I can create a Ghidra analyzer based on lessons learnt.
