# Rocksmith-DLC-mover

The purpose of this app is for those of us that use Rocksmith CDLC
The reality for a lot of us especially that stream is that we are
constantly moving the new charts to the DLC folder manually, this
can get quite tedious, so this offers some automation, as well as
a feature to sort and tidy up the folders

Features:
Auto movement from a set download location to a CDLC folder inside the DLC folder
Note: RS scans recursively so segregating CDLC is easy and offers a way to quickly "reset" without losing unlcoked content

A cleanup feature that looks for loose PSARC files in the CDLC folder
This feature parses the file name and assume Band_Song file naming. It will then
check for and move the file into a folder for that band, if there is no folder it will make it.

Preserve original file. In this case the program should create a copy of the original
leaving the original file intact in the download folder.

Batch Transfer, if you have downloaded several charts and weren't running in auto mode
you can hit start transfer and it will batch transder using the above features

Auto mode. Once activated this will listen to the OS event for the download folder
for new files, check if it is a PSARC, and if it is, move it to the CDLC folder.

Planned feature change:

The preserve original function will at some point be replaced with a backup library function
this will allow you to set a destination where it will clone the CDLC folder, and all new transfers
will also be backed up there as well. That way if you for some reason have to reinstall rocksmith or
otherwise lose the folders, reset them, whatever, you have a backup.

Hoping to implement that by revision b0.5.0
