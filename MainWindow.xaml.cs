using System.Windows;
using System.Windows.Data;

namespace SqlReportConstructor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            this.InitializeComponent();
        }

        private MeteoStationsReferenceDataSet _windowDataSet;
        private MeteoStationsReferenceDataSetTableAdapters.meteo_stations_referenceTableAdapter _windowTableAdapter;
        private CollectionViewSource _windowViewSource;

        private void StationReferenceWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this._windowDataSet = ((MeteoStationsReferenceDataSet)(this.FindResource("MeteoStationsReferenceDataSet")));
            // Load data into the table meteo_stations_reference. You can modify this code as needed.
            this._windowTableAdapter = new MeteoStationsReferenceDataSetTableAdapters.meteo_stations_referenceTableAdapter();
            var meteoStationsReferenceDataSet = this._windowDataSet ;
            if ( meteoStationsReferenceDataSet != null )
            {
                this._windowTableAdapter.Fill(
                    meteoStationsReferenceDataSet.meteo_stations_reference);
            }
            this._windowViewSource = ((CollectionViewSource)(this.FindResource("MeteoStationsReferenceViewSource")));
            var collectionViewSource = this._windowViewSource ;
            collectionViewSource?.View?.MoveCurrentToFirst();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            var windowViewSource = this._windowViewSource ;
            var windowViewSourceView = windowViewSource?.View ;
            if (windowViewSourceView?.CurrentPosition > 0)
            {
                windowViewSourceView.MoveCurrentToPrevious();
            }
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            var collectionViewSource = this._windowViewSource ;
            var collectionView = ( CollectionView ) collectionViewSource?.View ;
            var some = collectionView?.Count - 1;

            if (collectionView?.CurrentPosition < some )
            {
                collectionView.MoveCurrentToNext();
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            var meteoStationsReferenceDataSet = this._windowDataSet ;
            if ( meteoStationsReferenceDataSet != null )
            {
                this._windowTableAdapter?.Update(meteoStationsReferenceDataSet.meteo_stations_reference);
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var v = false;
            var m = this._windowViewSource != null ;
            System.Data.DataRowView dataRowView = null ;
            if (m)
            {
                v = this._windowViewSource.View != null;
            }
            if (v)
            {
                dataRowView = (System.Data.DataRowView)this._windowViewSource.View.CurrentItem;
                
            }
            var d = dataRowView != null ;
            System.Data.DataRow row = null ;
            object [ ] valuesArray = { } ;
            if (d)
            {
                row = dataRowView.Row;
            }
            var r = row != null ;
            if (r)
            {
                valuesArray = row.ItemArray;
            }

            var value = valuesArray[0];
            long recordIndex = 0 ;
            var i = value != null ;
            if (i)
            {
                recordIndex = (long)value ;
            }

            var meteoStationsReferenceTableAdapter = this._windowTableAdapter;
            if (( meteoStationsReferenceTableAdapter != null )
                && i)
            {
                meteoStationsReferenceTableAdapter.Delete(recordIndex);
                this.RefreshButton_Click
                    (
                        sender ,
                        e ) ;

                //var meteoStationsReferenceDataGrid = this.MeteoStationsReferenceDataGrid ;
                //if ( meteoStationsReferenceDataGrid?.ItemsSource != null )
                //{
                //    var collectionView = CollectionViewSource.GetDefaultView(
                //        meteoStationsReferenceDataGrid.ItemsSource) ;
                //    collectionView?.Refresh();
                //}
            }
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            var meteoStationsReferenceTableAdapter = this._windowTableAdapter;
            if ( meteoStationsReferenceTableAdapter != null )
            {
                meteoStationsReferenceTableAdapter.ClearBeforeFill = true;
                var meteoStationsReferenceDataSet = this._windowDataSet;
                if (meteoStationsReferenceDataSet != null)
                {
                    meteoStationsReferenceTableAdapter.Fill
                        (
                            meteoStationsReferenceDataSet.meteo_stations_reference);
                }
            }
        }
    }
}
