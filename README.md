## StackSizeController Configuration / Datafile Conversion Tool
This is a standalone Windows executable that when run opens a command prompt with instructions on how to convert the datafile / configuration file to the newest version. Currently only v3 to v4 is supported, if there is enough interest, I could do v2 to v4, for now that's unimplemented.

## How to Use
1. Download the zip file from Releases and extract that anywhere.
2. Copy your v3 datafile (oxide/data/StackSizeController.json) to where you extracted the zip file, in the same location as the exe.
3. Run the executable and follow the instructions.
4. For v3 to v4 conversion, a new file named StackSizeController_config.json is created which has default configuration values, with a populated IndividualItemStackSize list with the custom values defined in your v3 configuration.
5. Open that _config.json value, and ONLY copy the IndividualItemStackSize section and place/replace that in your configuration for v4.
6. Reload your plugin and everything is set.