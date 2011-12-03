My goal was to bind a WPF ContextMenu located in a ListBox to a command the MVVM way without any code-behind. I thought this should be a trivial thing. I was totally wrong. After spending several hours googling for a solution and failing to apply every single of them, I decided to ask guidance to my friend Mathieu who is a WPF genius. He found a cool solution which I publish here for the benefit of community.

### My message Mathieu: 

> I'm going crazy with a problem of ContextMenu in a ListBox. I am not able to bind a command to a context menu. I did a small project to reproduce my problem. I have a listbox bound on a view model. In this model I have a command that displays a message with the items selected from the list. I bound this command to three elements: a menu item in a menu bar, a button in a tool bar and finally a menu item the listbox context menu. The binding of the context menu does not work but it works for two other elements. I'm really desperate. It seems to be a known problem because the context menu is not in the visual tree. I am looking for a solution without code-behind and the ones I found on the web does not work for me (or I do not understand how they work). Therefore I ask your help when you have the time. 

> Thank you.

> Manuel

### His reply with the solution:

> This was not an easy one. I had to Google and try a lot. But I found a solution that works perfectly reasonably concise. I "pushed" the solution in your Git repo. Below is the new XAML with the changes.

> In short, as the ContextMenu is not part of the visual tree, it is very difficult to bind the ListBox from it. So I inverted a few conventions and I passed the ListBox in the DataContext of the ContextMenu. Then I am able find the command in the view model by accessing the DataContext of the ListBox. The clue I found on Google is to use the ContextMenu property PlacementTarget to access the ListBox. This is slightly twisted, but once you think about it, it's not that complicated. 

> Happy coding!

> Mathieu

    <Window x:Class="WpfListContextMenu.MainWindow" 
            xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
            xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
            xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
            xmlns:d="http://schemas.microsoft.com/expression/blend/2008" mc:Ignorable="d"
            xmlns:local="clr-namespace:WpfListContextMenu"
            d:DataContext="{d:DesignInstance IsDesignTimeCreatable=True, Type=local:MainWindowViewModel}" 
            Title="MainWindow" Height="350" Width="268">
      <Grid>
        <DockPanel>
          <Menu DockPanel.Dock="Top">
            <MenuItem Header="Show Selected" Command="{Binding ShowSelectedCommand, Mode=OneWay}"
                      CommandParameter="{Binding SelectedItems, ElementName=listBox}" />
          </Menu>
          <ToolBar DockPanel.Dock="Top">
            <ToolBar.Header>
              <Button Content="Show Selected" Command="{Binding ShowSelectedCommand, Mode=OneWay}"
                      CommandParameter="{Binding SelectedItems, ElementName=listBox}"/>
            </ToolBar.Header>
          </ToolBar>
          <ListBox x:Name="listBox" DockPanel.Dock="Top" ItemsSource="{Binding Items}" DisplayMemberPath="Name"
                   SelectionMode="Extended">
            <ListBox.ContextMenu>
              <ContextMenu DataContext="{Binding Path=PlacementTarget, RelativeSource={RelativeSource Self}}">
                <MenuItem Header="Show Selected" Command="{Binding Path=DataContext.ShowSelectedCommand}"
                          CommandParameter="{Binding Path=SelectedItems}" />
              </ContextMenu>
            </ListBox.ContextMenu>
          </ListBox>
        </DockPanel>
      </Grid>
    </Window>