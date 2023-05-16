/**
 * Copyright (c) Jason L Walker. All rights reserved.
 * Licensed under the MIT license.
 */
(function () {
    'use strict';

    function buildItem(editor, label, value) {
        return {
            type: 'menuitem',
            text: label,
            value: value,
            onAction: function (api) {
                editor.execCommand('mceInsertContent', false, value);
            }
        };
    }

    var global = tinymce.util.Tools.resolve('tinymce.PluginManager');
    var addReplacementMenu = function (editor) {
        var items = [];
        var placeholders = editor.getParam('placeholder_items', {});


        if (Object.keys(placeholders).length) {
            for (var key in placeholders) {
                items.push(buildItem(editor, key, placeholders[key] + ' '));
            }
        }

        if (items.length) {
            editor.ui.registry.addNestedMenuItem('placeholder', {
                text: 'Insert Placeholder',
                icon: 'template',
                getSubmenuItems: function () {
                    return items;
                }
            });
        }

    }

    function Plugin() {
        global.add('placeholder', function (editor) {
            addReplacementMenu(editor);
        });
    }

    Plugin();

}());
