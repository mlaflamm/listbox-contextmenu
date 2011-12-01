Salut Mathieu, je suis en train de devenir fou avec un probleme de ContextMenu dans un ListBox. 
Je ne suis pas capable de binder des commandes a un contect menu. J'ai fais un petit projet pour 
reproduire mon probleme. J'ai un ListBox bindé sur un view model. Dans ce view model j'ai une 
commande qui affiche un message avec les items selectionés dans la liste. J'ai bindé cette commande 
a trois elements: un menu item dans le menu bar, un bouton dans un tool bar et finalement dans un 
menu item dans le context menu de la liste. Le binding du context menu ne fonctionne pas mais ca 
fonctione pour les 2 autres items. Je suis vraiment desesperé. Il semble que ce soit un probleme connu 
a cause que le context menu n'est pas dans le visual tree. Je cherche un solution sans code-behind et 
celles que j'ai trouvé sur le web ne fonctionne pas (ou je ne comprend pas comment elles fonctionnent). 
C'est pourquoi je demande ton aide quand tu auras deux minutes. 

Merci. 

-Manuel

--

C’en était une vraiment pas facile.  J’ai dû Googler et expérimenter beaucoup.  Mais j’ai trouvé une solution 
raisonnablement concise qui fonctionne parfaitement.  J’ai "poussé" la solution dans ton Git. Voici 
ci-dessous le nouveau XAML avec les changements en vert. 

En résumé, comme le ContextMenu ne fait pas partie du visual tree, c’est très difficile de binder sur 
le list box à partir de celui-ci.  Alors j’ai inversé un peu les convention et j’ai passé le listbox 
dans le DataContext du ContextMenu.  Ensuite, je suis capable d’aller rechercher la commande dans le 
view model en accédant au DataContext de ce ListBox.  La piste que j’ai trouvée sur Google est d’utiliser 
la propriété PlacementTarget du ContextMenu pour accéder au ListBox.  C’est légèrement tordu, mais une 
fois qu’on y réfléchit bien, c’est pas si compliqué que ça.

Happy coding!

- Mathieu

--

<Window x:Class="WpfListContextMenu.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
              xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
              xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
              mc:Ignorable="d"
              xmlns:local="clr-namespace:WpfListContextMenu"
              d:DataContext="{d:DesignInstance IsDesignTimeCreatable=True, Type=local:MainWindowViewModel}"
        Title="MainWindow" Height="350" Width="268">
    <Grid>
       <DockPanel>
              <Menu DockPanel.Dock="Top">
                     <MenuItem Header="Show Selected" Command="{Binding ShowSelectedCommand, Mode=OneWay}" CommandParameter="{Binding SelectedItems, ElementName=listBox}"/>
              </Menu>
              <ToolBar DockPanel.Dock="Top">
                     <ToolBar.Header>
                           <Button Content="Show Selected" Command="{Binding ShowSelectedCommand, Mode=OneWay}" CommandParameter="{Binding SelectedItems, ElementName=listBox}" d:LayoutOverrides="HorizontalMargin"/>
                     </ToolBar.Header>
              </ToolBar>
              <ListBox x:Name="listBox" DockPanel.Dock="Top" ItemsSource="{Binding Items}" DisplayMemberPath="Name" SelectionMode="Extended">
                <ListBox.ContextMenu>
                    <ContextMenu DataContext="{Binding Path=PlacementTarget, RelativeSource={RelativeSource Self}}">
                        <MenuItem Header="Show Selected" Command="{Binding Path=DataContext.ShowSelectedCommand}"
                                  CommandParameter="{Binding Path=SelectedItems}"/>
                    </ContextMenu>
                </ListBox.ContextMenu>
                     </ListBox>
       </DockPanel>
    </Grid>
</Window>