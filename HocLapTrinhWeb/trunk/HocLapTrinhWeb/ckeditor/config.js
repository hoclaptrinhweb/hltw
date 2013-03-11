/*
Copyright (c) 2003-2012, CKSource - Frederico Knabben. All rights reserved.
For licensing, see LICENSE.html or http://ckeditor.com/license
*/

CKEDITOR.editorConfig = function (config) {
    // Define changes to default configuration here. For example:
    // config.language = 'fr';
    // config.uiColor = '#AADC6E';
    config.extraPlugins = 'syntaxhighlight';
    config.toolbar =
    [
        ["Source", "-", "Save", "NewPage", "Preview", "-", "Templates"],
        ["Cut", "Copy", "Paste", "PasteText", "PasteFromWord"],
        ["Undo", "Redo", "-", "Find", "Replace", "RemoveFormat"],
        ["Maximize", "ShowBlocks"],
        "/",
        ["JustifyLeft", "JustifyCenter", "JustifyRight", "JustifyBlock"],
        ["Bold", "Italic", "Underline", "Strike", "-", "Subscript", "Superscript", 'syntaxhighlight'],
        ["NumberedList", "BulletedList", "-", "Outdent", "Indent"],
        ["TextColor", "BGColor"],
        ["Link", "Unlink"],
        ["Image", "Flash", "Table", "HorizontalRule", "Smiley", "SpecialChar", "PageBreak", "Iframe"],
        "/",
        ["Styles", "Format", "Font", "FontSize"]
    ];
    config.filebrowserImageBrowseUrl = "../ckeditor/ImageBrowser.aspx";
    config.filebrowserImageWindowWidth = 780;
    config.filebrowserImageWindowHeight = 720;
    config.filebrowserBrowseUrl = "../ckeditor/LinkBrowser.aspx";
    config.filebrowserWindowWidth = 500;
    config.filebrowserWindowHeight = 650;
    config.htmlEncodeOutput = true;
};

