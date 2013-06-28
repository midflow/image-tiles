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
using System.IO;

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

            if (!App.blLoadIamge)
            {
                LoadTile();
            }
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            //string imgname = "";
            if (App.imgName != "")
            {
                IsolatedStorageFile file;
                IsolatedStorageFileStream stream;
                BitmapImage image;

                file = IsolatedStorageFile.GetUserStoreForApplication();
                stream = file.OpenFile("/Shared/ShellContent/" +App.imgName, FileMode.Open, FileAccess.Read);

                image = new BitmapImage();
                image.SetSource(stream);

                switch (App.imgName)
                {
                    case "wide.jpg":                        
                        imgWide.Source = image;                        
                        break;
                    case "medium.jpg":                        
                        imgMedium.Source = image;                       
                        break;
                    case "wide1.jpg":                        
                        imgWide1.Source = image;                        
                        break;
                    case "wide2.jpg":                        
                        imgWide2.Source = image;                       
                        break;
                    case "medium1.jpg":                         
                        imgMedium1.Source = image;                     
                        break;
                    case "medium2.jpg":                        
                        imgMedium2.Source = image;                       
                        break;
                }
                stream.Dispose();
            }
        }

       

        private void LoadTile()
        {
            try
            {
                Dictionary<string, string> dic = new Dictionary<string, string>();
                IsolatedStorageFile myIsolatedStorage = IsolatedStorageFile.GetUserStoreForApplication();
                if (myIsolatedStorage.FileExists("ImageTiles.ini"))
                {
                    IsolatedStorageFileStream fileStream = myIsolatedStorage.OpenFile("ImageTiles.ini", FileMode.Open, FileAccess.Read);
                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        while (!reader.EndOfStream)
                        {
                            string strFileContent = reader.ReadLine();
                            string[] linecontent = strFileContent.Split(' ');
                            dic.Add(linecontent[0], linecontent[1]);
                            //MessageBox.Show(strFileContent);
                        }

                        //set value to app
                        if (dic.ContainsKey("PanoramaIndex"))
                        {
                            int pnmIndex = int.Parse(dic["PanoramaIndex"]);
                            pnmImage.DefaultItem = pnmImage.Items[pnmIndex];

                            Stream stream;
                            System.Windows.Media.Imaging.BitmapImage bmp;
                            if (pnmIndex == 0)
                            {
                                txtImgtext.Text = dic["Title"];

                                stream = App.LoadFile(dic["BackgroundImage"].Replace("isostore:", ""));
                                bmp = new System.Windows.Media.Imaging.BitmapImage();
                                bmp.SetSource(stream);
                                imgMedium.Source = bmp;

                                //uri = new Uri(dic["WideBackgroundImage"], UriKind.Absolute);
                                stream = App.LoadFile(dic["WideBackgroundImage"].Replace("isostore:", ""));
                                bmp = new System.Windows.Media.Imaging.BitmapImage();
                                bmp.SetSource(stream);
                                imgWide.Source = bmp;
                                //imgSource = new BitmapImage(uri);
                                //imgWide.Source = imgSource;

                                blAnimation.IsChecked = bool.Parse(dic["Animation"]);
                            }
                            else
                            {
                                txtImgtext2.Text = dic["Title"];

                                stream = App.LoadFile(dic["BackgroundImage"].Replace("isostore:", ""));
                                bmp = new System.Windows.Media.Imaging.BitmapImage();
                                bmp.SetSource(stream);
                                imgMedium1.Source = bmp;

                                stream = App.LoadFile(dic["WideBackgroundImage"].Replace("isostore:", ""));
                                bmp = new System.Windows.Media.Imaging.BitmapImage();
                                bmp.SetSource(stream);
                                imgWide1.Source = bmp;

                                blAnimation2.IsChecked = bool.Parse(dic["Animation"]);

                                if (blAnimation2.IsChecked == true)
                                {
                                    stream = App.LoadFile(dic["BackBackgroundImage"].Replace("isostore:", ""));
                                    bmp = new System.Windows.Media.Imaging.BitmapImage();
                                    bmp.SetSource(stream);
                                    imgMedium2.Source = bmp;

                                    stream = App.LoadFile(dic["WideBackBackgroundImage"].Replace("isostore:", ""));
                                    bmp = new System.Windows.Media.Imaging.BitmapImage();
                                    bmp.SetSource(stream);
                                    imgWide2.Source = bmp;
                                }
                            }

                        }



                    }
                }
                else
                {
                    IsolatedStorageFileStream fileStream = myIsolatedStorage.OpenFile("ImageTiles.ini", FileMode.Create, FileAccess.Write);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
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

                App.saveImage((BitmapSource)bmp, "/Shared/ShellContent/wide.jpg");
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

                App.saveImage((BitmapSource)bmp, "/Shared/ShellContent/Medium.jpg");
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

                App.saveImage((BitmapSource)bmp, "/Shared/ShellContent/wide1.jpg");
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

                App.saveImage((BitmapSource)bmp, "/Shared/ShellContent/Medium1.jpg");
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

                App.saveImage((BitmapSource)bmp, "/Shared/ShellContent/wide2.jpg");
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

                App.saveImage((BitmapSource)bmp, "/Shared/ShellContent/Medium2.jpg");
                imgMedium2.Source = bmp;
            }
        }
       

        private static void SetProperty(object instance, string name, object value)
        {
            var setMethod = instance.GetType().GetProperty(name).GetSetMethod();
            setMethod.Invoke(instance, new object[] { value });
        }

        private void Button_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            //PhotoChooserTask photoChooserTask;
            //photoChooserTask = new PhotoChooserTask();
            //photoChooserTask.Completed += new EventHandler<PhotoResult>(imgWideChooserTask_Completed);
            //photoChooserTask.Show();
            NavigationService.Navigate(new Uri("/Page1.xaml?imagename=wide.jpg", UriKind.Relative));
        }

        private void Button_Tap_1(object sender, System.Windows.Input.GestureEventArgs e)
        {
            //PhotoChooserTask photoChooserTask;
            //photoChooserTask = new PhotoChooserTask();
            //photoChooserTask.Completed += new EventHandler<PhotoResult>(imgMediumChooserTask_Completed);
            //photoChooserTask.Show();
            NavigationService.Navigate(new Uri("/Page1.xaml?imagename=medium.jpg", UriKind.Relative));
        }

        private void btnWide21_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            //PhotoChooserTask photoChooserTask;
            //photoChooserTask = new PhotoChooserTask();
            //photoChooserTask.Completed += new EventHandler<PhotoResult>(imgWideChooserTask1_Completed);
            //photoChooserTask.Show();
            NavigationService.Navigate(new Uri("/Page1.xaml?imagename=wide1.jpg", UriKind.Relative));
        }

        private void btnWide22_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            //PhotoChooserTask photoChooserTask;
            //photoChooserTask = new PhotoChooserTask();
            //photoChooserTask.Completed += new EventHandler<PhotoResult>(imgWideChooserTask2_Completed);
            //photoChooserTask.Show();
            NavigationService.Navigate(new Uri("/Page1.xaml?imagename=wide2.jpg", UriKind.Relative));
        }

        private void btnMedium21_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            //PhotoChooserTask photoChooserTask;
            //photoChooserTask = new PhotoChooserTask();
            //photoChooserTask.Completed += new EventHandler<PhotoResult>(imgMediumChooserTask1_Completed);
            //photoChooserTask.Show();
            NavigationService.Navigate(new Uri("/Page1.xaml?imagename=medium1.jpg", UriKind.Relative));
        }

        private void bntMedium22_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            //PhotoChooserTask photoChooserTask;
            //photoChooserTask = new PhotoChooserTask();
            //photoChooserTask.Completed += new EventHandler<PhotoResult>(imgMediumChooserTask2_Completed);
            //photoChooserTask.Show();
            NavigationService.Navigate(new Uri("/Page1.xaml?imagename=medium2.jpg", UriKind.Relative));
        }

        private void ApplicationBarIconButton_Click(object sender, EventArgs e)
        {
            try
            {
                string strFileContent = "";
                if (pnmImage.SelectedIndex == 0)
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

                        strFileContent += "PanoramaIndex 0 \r\n";
                        strFileContent += "Title " + txtImgtext.Text + "\r\n";
                        strFileContent += "SmallBackgroundImage isostore:/Shared/ShellContent/medium.jpg \r\n";
                        strFileContent += "BackgroundImage isostore:/Shared/ShellContent/medium.jpg \r\n";
                        strFileContent += "WideBackgroundImage isostore:/Shared/ShellContent/wide.jpg \r\n";
                        strFileContent += "Animation " + blAnimation.IsChecked.ToString() + "\r\n";
                        if (blAnimation.IsChecked == true)
                        {
                            SetProperty(UpdateTileData, "BackBackgroundImage", new Uri("isostore:/Shared/ShellContent/Medium.jpg", UriKind.Absolute));
                            SetProperty(UpdateTileData, "WideBackBackgroundImage", new Uri("isostore:/Shared/ShellContent/wide.jpg", UriKind.Absolute));

                            strFileContent += "BackBackgroundImage isostore:/Shared/ShellContent/medium.jpg \r\n";
                            strFileContent += "WideBackBackgroundImage isostore:/Shared/ShellContent/wide.jpg \r\n";

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

                        WriteToFile(strFileContent);

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

                        strFileContent += "PanoramaIndex 0 \r\n";
                        strFileContent += "Title " + txtImgtext.Text + "\r\n";
                        strFileContent += "BackgroundImage isostore://Shared//ShellContent//medium.jpg \r\n";
                        strFileContent += "Animation " + blAnimation.IsChecked.ToString() + "\r\n";
                        if (blAnimation.IsChecked == true)
                        {
                            NewTileData.BackBackgroundImage = new Uri("isostore:/Shared/ShellContent/medium.jpg", UriKind.Absolute);
                            strFileContent += "BackBackgroundImage isostore:/Shared/ShellContent/medium.jpg \r\n";
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
                else
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

                        strFileContent += "PanoramaIndex 1 \r\n";
                        strFileContent += "Title " + txtImgtext2.Text + "\r\n";
                        strFileContent += "SmallBackgroundImage isostore:/Shared/ShellContent/medium1.jpg \r\n";
                        strFileContent += "BackgroundImage isostore:/Shared/ShellContent/medium1.jpg \r\n";
                        strFileContent += "WideBackgroundImage isostore:/Shared/ShellContent/wide1.jpg \r\n";
                        strFileContent += "Animation " + blAnimation.IsChecked.ToString() + "\r\n";

                        if (blAnimation2.IsChecked == true)
                        {
                            SetProperty(UpdateTileData, "BackBackgroundImage", new Uri("isostore:/Shared/ShellContent/Medium2.jpg", UriKind.Absolute));
                            SetProperty(UpdateTileData, "WideBackBackgroundImage", new Uri("isostore:/Shared/ShellContent/wide2.jpg", UriKind.Absolute));

                            strFileContent += "BackBackgroundImage isostore:/Shared/ShellContent/medium2.jpg \r\n";
                            strFileContent += "WideBackBackgroundImage isostore:/Shared/ShellContent/wide2.jpg \r\n";

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
                            Title = txtImgtext2.Text,
                            Count = 0,
                        };

                        strFileContent += "PanoramaIndex 1 \r\n";
                        strFileContent += "Title " + txtImgtext2.Text + "\r\n";
                        strFileContent += "BackgroundImage isostore:/Shared/ShellContent/medium1.jpg \r\n";
                        strFileContent += "Animation " + blAnimation.IsChecked.ToString() + "\r\n";

                        if (blAnimation2.IsChecked == true)
                        {
                            NewTileData.BackBackgroundImage = new Uri("isostore:/Shared/ShellContent/medium2.jpg", UriKind.Absolute);
                            strFileContent += "BackBackgroundImage isostore:/Shared/ShellContent/medium.jpg \r\n";
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
                WriteToFile(strFileContent);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void RateButton_Click(object sender, EventArgs e)
        {
            MarketplaceReviewTask review = new MarketplaceReviewTask();
            review.Show();
        }

        private void BuyButton_Click(object sender, EventArgs e)
        {
            MarketplaceDetailTask marketplaceDetailTask = new MarketplaceDetailTask();

            marketplaceDetailTask.ContentIdentifier = "c743bdf5-620a-42ef-a493-4793aa400668";
            marketplaceDetailTask.ContentType = MarketplaceContentType.Applications;

            marketplaceDetailTask.Show();
        }

        private void WriteToFile(string strFileContent)
        {
            try
            {
                IsolatedStorageFile myIsolatedStorage = IsolatedStorageFile.GetUserStoreForApplication();

                //Open existing file
                IsolatedStorageFileStream fileStream = myIsolatedStorage.OpenFile("ImageTiles.ini", FileMode.OpenOrCreate, FileAccess.Write);
                using (StreamWriter writer = new StreamWriter(fileStream))
                {
                    //string someTextData = "Some More TEXT Added:  !";
                    writer.Write(strFileContent);
                    writer.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
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