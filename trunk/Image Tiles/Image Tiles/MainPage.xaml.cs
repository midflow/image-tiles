using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Tasks;
using System.Windows.Media.Imaging;
using System.IO.IsolatedStorage;
using Microsoft.Phone.Shell;

namespace Image_Tiles
{
    public partial class MainPage : PhoneApplicationPage
    {
        private static Version TargetedVersion = new Version(7, 10, 8858);
        public static bool IsTargetedVersion { get { return Environment.OSVersion.Version >= TargetedVersion; } }

        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Set the data context of the listbox control to the sample data
            DataContext = App.ViewModel;
            this.Loaded += new RoutedEventHandler(MainPage_Loaded);
        }

        // Load data for the ViewModel Items
        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (!App.ViewModel.IsDataLoaded)
            {
                App.ViewModel.LoadData();
            }
        }

        void imgWideChooserTask_Completed(object sender, PhotoResult e)
        {
            if (e.TaskResult == TaskResult.OK)
            {
                //MessageBox.Show(e.ChosenPhoto.Length.ToString());
                //Pic1.Source = e.ChosenPhoto;
                //Code to display the photo on the page in an image control named myImage.
                System.Windows.Media.Imaging.BitmapImage bmp = new System.Windows.Media.Imaging.BitmapImage();
                bmp.SetSource(e.ChosenPhoto);

                saveImage((BitmapSource)bmp, "/Shared/ShellContent/wide.jpg");
                imgWide.Source = bmp;
            }
        }

        void imgMediumChooserTask_Completed(object sender, PhotoResult e)
        {
            if (e.TaskResult == TaskResult.OK)
            {
                //MessageBox.Show(e.ChosenPhoto.Length.ToString());
                //Pic1.Source = e.ChosenPhoto;
                //Code to display the photo on the page in an image control named myImage.
                System.Windows.Media.Imaging.BitmapImage bmp = new System.Windows.Media.Imaging.BitmapImage();
                bmp.SetSource(e.ChosenPhoto);

                saveImage((BitmapSource)bmp, "/Shared/ShellContent/Medium.jpg");
                imgMedium.Source = bmp;
            }
        }

        void imgWideChooserTask1_Completed(object sender, PhotoResult e)
        {
            if (e.TaskResult == TaskResult.OK)
            {
                //MessageBox.Show(e.ChosenPhoto.Length.ToString());
                //Pic1.Source = e.ChosenPhoto;
                //Code to display the photo on the page in an image control named myImage.
                System.Windows.Media.Imaging.BitmapImage bmp = new System.Windows.Media.Imaging.BitmapImage();
                bmp.SetSource(e.ChosenPhoto);

                saveImage((BitmapSource)bmp, "/Shared/ShellContent/wide1.jpg");
                imgWide1.Source = bmp;
            }
        }

        void imgMediumChooserTask1_Completed(object sender, PhotoResult e)
        {
            if (e.TaskResult == TaskResult.OK)
            {
                //MessageBox.Show(e.ChosenPhoto.Length.ToString());
                //Pic1.Source = e.ChosenPhoto;
                //Code to display the photo on the page in an image control named myImage.
                System.Windows.Media.Imaging.BitmapImage bmp = new System.Windows.Media.Imaging.BitmapImage();
                bmp.SetSource(e.ChosenPhoto);

                saveImage((BitmapSource)bmp, "/Shared/ShellContent/Medium1.jpg");
                imgMedium1.Source = bmp;
            }
        }

        void imgWideChooserTask2_Completed(object sender, PhotoResult e)
        {
            if (e.TaskResult == TaskResult.OK)
            {
                //MessageBox.Show(e.ChosenPhoto.Length.ToString());
                //Pic1.Source = e.ChosenPhoto;
                //Code to display the photo on the page in an image control named myImage.
                System.Windows.Media.Imaging.BitmapImage bmp = new System.Windows.Media.Imaging.BitmapImage();
                bmp.SetSource(e.ChosenPhoto);

                saveImage((BitmapSource)bmp, "/Shared/ShellContent/wide2.jpg");
                imgWide2.Source = bmp;
            }
        }

        void imgMediumChooserTask2_Completed(object sender, PhotoResult e)
        {
            if (e.TaskResult == TaskResult.OK)
            {
                //MessageBox.Show(e.ChosenPhoto.Length.ToString());
                //Pic1.Source = e.ChosenPhoto;
                //Code to display the photo on the page in an image control named myImage.
                System.Windows.Media.Imaging.BitmapImage bmp = new System.Windows.Media.Imaging.BitmapImage();
                bmp.SetSource(e.ChosenPhoto);

                saveImage((BitmapSource)bmp, "/Shared/ShellContent/Medium2.jpg");
                imgMedium2.Source = bmp;
            }
        }
        private void saveImage(BitmapSource bmpsource, string imgName)
        {
            try
            {
                using (IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    if (isf.FileExists(imgName))
                        isf.DeleteFile(imgName);
                    using (IsolatedStorageFileStream isfs = isf.CreateFile(imgName))
                    {
                        var bmp = new WriteableBitmap(bmpsource);
                        bmp.SaveJpeg(isfs, bmp.PixelWidth, bmp.PixelHeight, 0, 100);
                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

       
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            PhotoChooserTask photoChooserTask;
            photoChooserTask = new PhotoChooserTask();
            photoChooserTask.Completed += new EventHandler<PhotoResult>(imgWideChooserTask_Completed);
            photoChooserTask.Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            PhotoChooserTask photoChooserTask;
            photoChooserTask = new PhotoChooserTask();
            photoChooserTask.Completed += new EventHandler<PhotoResult>(imgMediumChooserTask_Completed);
            photoChooserTask.Show();
        }
        private void Button_Click_32(object sender, RoutedEventArgs e)
        {
            // Create the tile if we didn't find it already exists.
            // Create the tile object and set some initial properties for the tile.
            // The Count value of 12 will show the number 12 on the front of the Tile. Valid values are 1-99.
            // A Count value of 0 will indicate that the Count should not be displayed.
            //var uri = new Uri("WP_000725.jpg", UriKind.Absolute);
            if (IsTargetedVersion)
            {

                // Get the new FlipTileData type.
                Type flipTileDataType = Type.GetType("Microsoft.Phone.Shell.FlipTileData, Microsoft.Phone");

                // Get the ShellTile type so we can call the new version of "Update" that takes the new Tile templates.
                Type shellTileType = Type.GetType("Microsoft.Phone.Shell.ShellTile, Microsoft.Phone");

                // Loop through any existing Tiles that are pinned to Start.
                var tileToUpdate = ShellTile.ActiveTiles.FirstOrDefault(x => x.NavigationUri.ToString().Contains("MainPage.xaml"));
                // Get the constructor for the new FlipTileData class and assign it to our variable to hold the Tile properties.
                var UpdateTileData = flipTileDataType.GetConstructor(new Type[] { }).Invoke(null);

                // Set the properties. 
                SetProperty(UpdateTileData, "Title", txtImgtext2.Text);
                //SetProperty(UpdateTileData, "Count", 12);
                //SetProperty(UpdateTileData, "BackTitle", "Back Tile Title");
                //SetProperty(UpdateTileData, "BackContent", "Content For back tile.");
                SetProperty(UpdateTileData, "SmallBackgroundImage", new Uri("isostore:/Shared/ShellContent/medium1.jpg", UriKind.Absolute));
                SetProperty(UpdateTileData, "BackgroundImage", new Uri("isostore:/Shared/ShellContent/Medium1.jpg", UriKind.Absolute));
                SetProperty(UpdateTileData, "WideBackgroundImage", new Uri("isostore:/Shared/ShellContent/wide1.jpg", UriKind.Absolute));
                if (blAnimation2.IsChecked == true)
                {
                    SetProperty(UpdateTileData, "BackBackgroundImage", new Uri("isostore:/Shared/ShellContent/Medium2.jpg", UriKind.Absolute));
                    SetProperty(UpdateTileData, "WideBackBackgroundImage", new Uri("isostore:/Shared/ShellContent/wide2.jpg", UriKind.Absolute));
                }

                if (tileToUpdate == null)
                {
                    Uri tilesUri = new Uri("/MainPage.xaml", UriKind.Relative);
                    ShellTileExt.Create(tilesUri, (ShellTileData)UpdateTileData, true);
                }
                else
                {
                    // Invoke the new version of ShellTile.Update.
                    shellTileType.GetMethod("Update").Invoke(tileToUpdate, new Object[] { UpdateTileData });
                    MessageBox.Show("Your Image Tiles is updated");
                }

            }
            else
            {
                ShellTile TileToFind = ShellTile.ActiveTiles.FirstOrDefault(x => x.NavigationUri.ToString().Contains("MainPage.xaml"));
                StandardTileData NewTileData = new StandardTileData
                {
                    BackgroundImage = new Uri("isostore:/Shared/ShellContent/medium1.jpg", UriKind.Absolute),
                    Title = txtImgtext.Text,
                    Count = 0,
                };

                if (blAnimation2.IsChecked == true)
                {
                    NewTileData.BackBackgroundImage = new Uri("isostore:/Shared/ShellContent/medium2.jpg", UriKind.Absolute);
                }


                if (TileToFind == null)
                {
                    // Create the tile and pin it to Start. This will cause a navigation to Start and a deactivation of our application.
                    Uri tilesUri = new Uri("/MainPage.xaml", UriKind.Relative);
                    ShellTile.Create(tilesUri, NewTileData);
                }
                else
                {

                    TileToFind.Update(NewTileData);
                    MessageBox.Show("Your Image Tiles is updated");
                }

            }
        }
        private static void SetProperty(object instance, string name, object value)
        {
            var setMethod = instance.GetType().GetProperty(name).GetSetMethod();
            setMethod.Invoke(instance, new object[] { value });
        }

        private void Button_Click_12(object sender, RoutedEventArgs e)
        {
            PhotoChooserTask photoChooserTask;
            photoChooserTask = new PhotoChooserTask();
            photoChooserTask.Completed += new EventHandler<PhotoResult>(imgWideChooserTask2_Completed);
            photoChooserTask.Show();
        }

        private void Button_Click_21(object sender, RoutedEventArgs e)
        {
            PhotoChooserTask photoChooserTask;
            photoChooserTask = new PhotoChooserTask();
            photoChooserTask.Completed += new EventHandler<PhotoResult>(imgMediumChooserTask1_Completed);
            photoChooserTask.Show();
        }

        private void Button_Click_22(object sender, RoutedEventArgs e)
        {
            PhotoChooserTask photoChooserTask;
            photoChooserTask = new PhotoChooserTask();
            photoChooserTask.Completed += new EventHandler<PhotoResult>(imgMediumChooserTask2_Completed);
            photoChooserTask.Show();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            // Create the tile if we didn't find it already exists.
            // Create the tile object and set some initial properties for the tile.
            // The Count value of 12 will show the number 12 on the front of the Tile. Valid values are 1-99.
            // A Count value of 0 will indicate that the Count should not be displayed.
            //var uri = new Uri("WP_000725.jpg", UriKind.Absolute);
            if (IsTargetedVersion)
            {

                // Get the new FlipTileData type.
                Type flipTileDataType = Type.GetType("Microsoft.Phone.Shell.FlipTileData, Microsoft.Phone");

                // Get the ShellTile type so we can call the new version of "Update" that takes the new Tile templates.
                Type shellTileType = Type.GetType("Microsoft.Phone.Shell.ShellTile, Microsoft.Phone");

                // Loop through any existing Tiles that are pinned to Start.
                var tileToUpdate = ShellTile.ActiveTiles.FirstOrDefault(x => x.NavigationUri.ToString().Contains("MainPage.xaml"));
                // Get the constructor for the new FlipTileData class and assign it to our variable to hold the Tile properties.
                var UpdateTileData = flipTileDataType.GetConstructor(new Type[] { }).Invoke(null);

                // Set the properties. 
                SetProperty(UpdateTileData, "Title", txtImgtext.Text);
                //SetProperty(UpdateTileData, "Count", 12);
                //SetProperty(UpdateTileData, "BackTitle", "Back Tile Title");
                //SetProperty(UpdateTileData, "BackContent", "Content For back tile.");
                SetProperty(UpdateTileData, "SmallBackgroundImage", new Uri("isostore:/Shared/ShellContent/medium.jpg", UriKind.Absolute));
                SetProperty(UpdateTileData, "BackgroundImage", new Uri("isostore:/Shared/ShellContent/Medium.jpg", UriKind.Absolute));
                SetProperty(UpdateTileData, "WideBackgroundImage", new Uri("isostore:/Shared/ShellContent/wide.jpg", UriKind.Absolute));
                if (blAnimation.IsChecked == true)
                {
                    SetProperty(UpdateTileData, "BackBackgroundImage", new Uri("isostore:/Shared/ShellContent/Medium.jpg", UriKind.Absolute));
                    SetProperty(UpdateTileData, "WideBackBackgroundImage", new Uri("isostore:/Shared/ShellContent/wide.jpg", UriKind.Absolute));
                }

                if (tileToUpdate == null)
                {
                    Uri tilesUri = new Uri("/MainPage.xaml", UriKind.Relative);
                    ShellTileExt.Create(tilesUri, (ShellTileData)UpdateTileData, true);
                }
                else
                {
                    // Invoke the new version of ShellTile.Update.
                    shellTileType.GetMethod("Update").Invoke(tileToUpdate, new Object[] { UpdateTileData });
                    MessageBox.Show("Your Image Tiles is updated");
                }

            }
            else
            {
                ShellTile TileToFind = ShellTile.ActiveTiles.FirstOrDefault(x => x.NavigationUri.ToString().Contains("MainPage.xaml"));
                StandardTileData NewTileData = new StandardTileData
                {
                    BackgroundImage = new Uri("isostore:/Shared/ShellContent/medium.jpg", UriKind.Absolute),
                    Title = txtImgtext.Text,
                    Count = 0,
                };

                if (blAnimation.IsChecked == true)
                {
                    NewTileData.BackBackgroundImage = new Uri("isostore:/Shared/ShellContent/medium.jpg", UriKind.Absolute);
                }


                if (TileToFind == null)
                {
                    // Create the tile and pin it to Start. This will cause a navigation to Start and a deactivation of our application.
                    Uri tilesUri = new Uri("/MainPage.xaml", UriKind.Relative);
                    ShellTile.Create(tilesUri, NewTileData);
                }
                else
                {

                    TileToFind.Update(NewTileData);
                    MessageBox.Show("Your Image Tiles is updated");
                }

            }

        }

        private void Button_Click_11(object sender, RoutedEventArgs e)
        {
            PhotoChooserTask photoChooserTask;
            photoChooserTask = new PhotoChooserTask();
            photoChooserTask.Completed += new EventHandler<PhotoResult>(imgWideChooserTask1_Completed);
            photoChooserTask.Show();
        }

        private void StackPanel_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            PhotoChooserTask photoChooserTask;
            photoChooserTask = new PhotoChooserTask();
            photoChooserTask.Completed += new EventHandler<PhotoResult>(imgWideChooserTask1_Completed);
            photoChooserTask.Show();
        }        

        private void Button_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            PhotoChooserTask photoChooserTask;
            photoChooserTask = new PhotoChooserTask();
            photoChooserTask.Completed += new EventHandler<PhotoResult>(imgWideChooserTask_Completed);
            photoChooserTask.Show();
        }
    }
    public static class ShellTileExt
    {
        public static void Create(Uri uri, ShellTileData tiledata, bool usewide)
        {
            Type shellTileType = Type.GetType("Microsoft.Phone.Shell.ShellTile, Microsoft.Phone");
            var createmethod = shellTileType.GetMethod("Create", new Type[] { typeof(Uri), typeof(ShellTileData), typeof(bool) });
            createmethod.Invoke(null, new object[] { uri, tiledata, usewide });
        }
    }
}