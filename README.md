# Nop.Plugin.Widgets.SpecialOrder
This is a nopCommerce widget plugin that adds support for special order products requiring a down-payment for purchase, with the remainder billed at delivery.

This is done via a technical specification attribute, whose value should be the full price of the product, while the displayed price is a down-payment only. The plugin displays such information in the catalog page as well as the product details page.

# Building
## Directory structure
The repository contains separate directories for each supported nopCommerce version, e.g. 4.60, 4.70, and so on.

## Common folder
The Common folder contains most of the common source code for each nopCommerce version of the plugin. Files are referenced (linked) from there.

## NopCommerce source
Each *Nop.Plugin.Widgets.SpecialOrder* project contains references to nopCommerce projects. For convenience the nopCommerce files are references via drive *n:*. It is recommended to map n: to the location of the nopCommerce source. For example, if nopCommerce source files are available in *c:\dev\nopCommerce public releases*, and each version in a subdirectory named by the release (i.e. *nopCommerce 4.20*) then the following command will map n: to that location

    net use n: "\\mycomputer\c$\dev\nopCommerce public releases" /persistent:yes

where mycomputer is the name of the development machine.
 
 ## Build the projects
 To build the projects, either open the preferred solution (.sln) file, or execute the build-all.cmd file in the root directory. That command builds all versions both in debug and release mode.

The output of the build, whether from Visual Studio, or from the build-all.cmd file, goes to the *_build* directory in the root directory
