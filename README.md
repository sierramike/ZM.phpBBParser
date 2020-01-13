# ZM.phpBBParser
Download phpBB2 posts from ForumActif "Le Forum du N"

This application has been developped in french as it targets french users.

After some forum members requests and my own needs, here is an app designed to backup forum posts to local hard drive, including pictures or not.
This app has been designed for "Le Forum du N" http://le-forum-du-n.1fr1.net/ but may be tested and/or modified to be used on other phpBB2 forums.

It can be compiled using Visual Studio 2017 or later, including Community version.

Download here : https://github.com/sierramike/ZM.phpBBParser/releases/download/1.0/ZM.phpBBParser-1.0.zip

- Download ZIP archive
- Unzip to folder of your choice
- Start "ZM.phpBBParser.exe" and use
* Upon closing, settings are saved into "Settings.xml" file.

# User Interface:
![User interface](https://i.servimg.com/u/f11/19/35/07/37/mainwi11.png)

# Parameters:
- Racine du site : the site root address (of the welcome page, don't modify unless the forum changes its URL !)
- Traitement des feuilles de style : what to do with cascading style sheets (see below)
- Traitement des images : what to do with images (see below)
- Dossier d'enregistrement : folder in which the backed up pages will be stored on the local hard drive. Use "..." to browse your computer.
- Nom du fichier de sortie : allows to choose between following naming rules for the backed up file names: upon page address (eg. t0001-titre-du-post.html), upon page title (eg. Titre du post.html), or a custom fixed file name.

# Cascading style sheet and images processing:
- Ne rien faire : leave backed up pages point to online pictures.
- Télécharger les fichiers : saves pictures to local hard drive and edits the backed up files to point to local versions.
- Inclure dans le fichier de sortie : saves pictures as embedded into the backed up page using base64 encoding. This setting may appear seducing because it produces a self-working html file, its size may quickly grow as it stores every single picture (even if present multiple times in the page) individually into the html code, with a size being up to 33% greater than original, due to base64 encoding. This option is not recommanded unless you know what you do.

The application, once opened, will monitor web pages URLs that are copied from the browser (either via right clic -> Copy link address, or by selcting the address bar and pressing Ctrl+C).
As soon as an URL matches the forum address, it is immediately displayed in the "Addresse de la page" field.
Just clic on "Télécharger" to start downloading the page to the local hard drive using the settings you chose.


# Check box "Télécharger automatiquement les adresses détectées":
This option will automatically launch the download as soon as a new address is detected (copied from the browser).

That way, you can simply launch the app and leave it in background, then browse the forum. Each time you want to backup a thread, just copy its link and it will be immediately backed up. Starting with Windows 7, progress is displayed on the taskbar icon in green, this allows to monitor the download progress when the application is left in the background.

The icon will flash red if something goes wrong while backing up.


Some bugs may subsist, feel free to add feedback, I will do my best to solve them!

Happy backup to all!

Original french version : http://le-forum-du-n.1fr1.net/t32743-zm-phpbbparser-archivage-des-posts-du-forum-sur-pc-version-1-0
