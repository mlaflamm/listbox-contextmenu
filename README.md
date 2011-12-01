Salut Mathieu, je suis en train de devenir fou avec un problème de ContextMenu dans un ListBox. Je ne suis pas capable de binder des commandes à un context menu. J'ai fait un petit projet pour reproduire mon problème. J'ai un ListBox bindé sur un view model. Dans ce view model j'ai une commande qui affiche un message avec les items sélectionnés dans la liste. J'ai bindé cette commande a trois éléments: un menu item dans le menu bar, un bouton dans un tool bar et finalement dans un menu item dans le context menu de la liste. Le binding du context menu ne fonctionne pas mais ça fonctionne pour les 2 autres items. Je suis vraiment désespéré. Il semble que ce soit un problème connu à cause que le context menu n'est pas dans le visual tree. Je cherche une solution sans code-behind et celles que j'ai trouvé sur le web ne fonctionne pas (ou je ne comprends pas comment elles fonctionnent). C'est pourquoi je demande ton aide quand tu auras deux minutes. 

Merci. 

-Manuel

--

C’en était une vraiment pas facile.  J’ai dû Googler et expérimenter beaucoup.  Mais j’ai trouvé une solution raisonnablement concise qui fonctionne parfaitement.  J’ai "poussé" la solution dans ton Git. Voici ci-dessous le nouveau XAML avec les changements en vert. 

En résumé, comme le ContextMenu ne fait pas partie du visual tree, c’est très difficile de binder sur le list box à partir de celui-ci.  Alors j’ai inversé un peu les conventions et j’ai passé le listbox dans le DataContext du ContextMenu.  Ensuite, je suis capable d’aller rechercher la commande dans le view model en accédant au DataContext de ce ListBox.  La piste que j’ai trouvée sur Google est d’utiliser la propriété PlacementTarget du ContextMenu pour accéder au ListBox.  C’est légèrement tordu, mais une fois qu’on y réfléchit bien, ce n’est pas si compliqué que ça.

Happy coding!

-Mathieu