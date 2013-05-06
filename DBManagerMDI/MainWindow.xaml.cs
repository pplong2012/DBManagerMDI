using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


//add
using System.Data;
using System.Data.Objects;
using AvalonDock;
using System.Collections.ObjectModel;
using DBManagerMDI.Model;
using System.Data.Metadata.Edm;

using DBManagerMDI.ViewModel;




namespace DBManagerMDI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       

        private DBManagerMDI.myschemaEntities _myschemaEntities = new DBManagerMDI.myschemaEntities();


        public ObservableCollection<DBTable> MySchemaDBTables
        {
            get { return (ObservableCollection<DBTable>)GetValue(MySchemaDBTablesProperty); }
            set { SetValue(MySchemaDBTablesProperty, value); }
        }

        public static readonly DependencyProperty MySchemaDBTablesProperty =
            DependencyProperty.Register("MySchemaDBTables", typeof(ObservableCollection<DBTable>),
            typeof(MainWindow), new UIPropertyMetadata(null));


        public MainWindow()
        {
            BuildPeopleTree();
            InitializeComponent();
            this.DataContext = this;

        }//MainWindow()

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            //var getAllUserData = from data in _myschemaEntities.user select data;
	        // this.dataGridShow.ItemsSource = getAllUserData;

            this.dataGridShow.ItemsSource = _myschemaEntities.user;
            UnitFunClass.SetDataGridReadOnly(this.dataGridShow, this.textboxShowInfo);
        }

		

        private void ChangeColor(object sender, RoutedEventArgs e)
        {
            ThemeFactory.ChangeColors((Color)ColorConverter.ConvertFromString(((MenuItem)sender).Header.ToString()));

        }

        private void ChangeStandardTheme(object sender, RoutedEventArgs e)
        {
            string name = (string)((MenuItem)sender).Tag;
            ThemeFactory.ChangeTheme(name);

        }


        private void buttonEdit_Click(object sender, RoutedEventArgs e)
        {
            System.String strShowInfo = System.String.Empty;

            //use can edit dataGrid
            this.dataGridShow.IsReadOnly = false;
            this.dataGridShow.CanUserAddRows = true;
            this.dataGridShow.CanUserDeleteRows = true;

			strShowInfo = "User Can Modify, AddRows or DeleteRows Now";
   			UnitFunClass.ShowInfoReSet(this.textboxShowInfo, strShowInfo);
            
        }//buttonEdit_Click

        private void buttonApplyChanges_Click(object sender, RoutedEventArgs e)
        {
            System.Int32 nRowAffected = 0;
            System.String strShowInfo = System.String.Empty;

            try
            {
                try
                {
                    // Try to save changes, which may cause a conflict.
                    nRowAffected = _myschemaEntities.SaveChanges();
					UnitFunClass.ShowInfoRowAffected(this.dataGridShow, this.textboxShowInfo, nRowAffected);	
                }
                catch (System.Data.OptimisticConcurrencyException ex)
                {
                    //refresh data
                    var varChanged = _myschemaEntities.ObjectStateManager.GetObjectStateEntries(EntityState.Modified | EntityState.Added | EntityState.Deleted);
                    foreach (var itemChanged in varChanged)
                    {
                        _myschemaEntities.Refresh(System.Data.Objects.RefreshMode.StoreWins, itemChanged.Entity);
                    }

                    // Save changes.
                    _myschemaEntities.SaveChanges();

					UnitFunClass.ShowInfoError(this.textboxShowInfo, ex.ToString());
					UnitFunClass.SetDataGridReadOnly(this.dataGridShow, this.textboxShowInfo);

                }
            }
            catch (UpdateException ex)
            {
				UnitFunClass.ShowInfoError(this.textboxShowInfo, ex.ToString());
            }
            catch
            {
                strShowInfo = "handled UnKnow Error!";
				UnitFunClass.ShowInfoError(this.textboxShowInfo, strShowInfo);
            }

        }//buttonApplyChanges_Click

        private void buttonDiscardChanges_Click(object sender, RoutedEventArgs e)
        {
            System.String strShowInfo = System.String.Empty;

            //refresh data
            var varChanged = _myschemaEntities.ObjectStateManager.GetObjectStateEntries(EntityState.Modified | EntityState.Added | EntityState.Deleted);
            foreach (var itemChanged in varChanged)
            {
                _myschemaEntities.Refresh(System.Data.Objects.RefreshMode.StoreWins, itemChanged.Entity);
            }

            // m_myschemaEntities.Refresh(System.Data.Objects.RefreshMode.StoreWins, m_myschemaEntities.user);
            _myschemaEntities.AcceptAllChanges();
            strShowInfo = "Discard ALL Changes";
			UnitFunClass.ShowInfoReSet(this.textboxShowInfo, strShowInfo);

            
            //refresh data in dataGrid
            this.dataGridShow.ItemsSource = null;
            var getAllUserData = from data in _myschemaEntities.user select data;
            this.dataGridShow.ItemsSource = getAllUserData;

        }//Window_Loaded

        private void BuildPeopleTree()
        {
            System.String tableSchema = System.String.Empty;
            System.String tableName = System.String.Empty;
            List<System.String> lstTableName = new List<System.String>();

           

            tableName = _myschemaEntities.account.EntitySet.ElementType.Name;//
            lstTableName.Add(tableName);
            tableName = _myschemaEntities.account_group.EntitySet.ElementType.Name;//
            lstTableName.Add(tableName);
            tableName = _myschemaEntities.currency.EntitySet.ElementType.Name;//
            lstTableName.Add(tableName);
            tableName = _myschemaEntities.exchange.EntitySet.ElementType.Name;//
            lstTableName.Add(tableName);
            tableName = _myschemaEntities.instrument.EntitySet.ElementType.Name;//
            lstTableName.Add(tableName);
            tableName = _myschemaEntities.instrument_type.EntitySet.ElementType.Name;//
            lstTableName.Add(tableName);
            tableName = _myschemaEntities.instrument_type_group.EntitySet.ElementType.Name;//
            lstTableName.Add(tableName);
            tableName = _myschemaEntities.limit.EntitySet.ElementType.Name;//
            lstTableName.Add(tableName);
            tableName = _myschemaEntities.organisation.EntitySet.ElementType.Name;//
            lstTableName.Add(tableName);
            tableName = _myschemaEntities.rating.EntitySet.ElementType.Name;//
            lstTableName.Add(tableName);
            tableName = _myschemaEntities.routing_rule.EntitySet.ElementType.Name;//
            lstTableName.Add(tableName);

            tableName = _myschemaEntities.user.EntitySet.ElementType.Name;//"user"
            lstTableName.Add(tableName);

            tableName = _myschemaEntities.user_account_relation.EntitySet.ElementType.Name;//
            lstTableName.Add(tableName);
            tableName = _myschemaEntities.user_instrument_subscription.EntitySet.ElementType.Name;//
            lstTableName.Add(tableName);


            //add node to treeview
            MySchemaDBTables = new ObservableCollection<DBTable>();
            foreach (var itemTableName in lstTableName)
            {
                MySchemaDBTables.Add(new DBTable() { Name = itemTableName });
            }




           //MySchemaDBTables.Add(new DBTable() { Name = "account" });
           // MySchemaDBTables[0].Subordinates.Add(new TreeViewNode() { Name = "user_instrument_subscription" });

        }

        private void treeviewSchemata_Selected(object sender, RoutedEventArgs e)
        {
            //which treeViewItem User chick
            System.String strShowInfo = System.String.Empty;//"user"
            System.String tableName = System.String.Empty;//"user"

            DBTable userSelected = (DBTable)this.treeviewSchemata.SelectedItem;
            System.String strItemNameGet = userSelected.Name;//"user"

            strShowInfo = "User Selected Table Name :  " + strItemNameGet;
			UnitFunClass.ShowInfoReSet(this.textboxShowInfo, strShowInfo);

            



            //tableName = _myschemaEntities.user.EntitySet.ElementType.Name;//"account"
            if (strItemNameGet == _myschemaEntities.account.EntitySet.ElementType.Name)
            {
                //var getAllUserData = from data in _myschemaEntities.user select data;
                this.dataGridShow.ItemsSource = _myschemaEntities.account;
            }
            else if (strItemNameGet == _myschemaEntities.account_group.EntitySet.ElementType.Name)
            {
                this.dataGridShow.ItemsSource = _myschemaEntities.account_group;
            }
            else if (strItemNameGet == _myschemaEntities.currency.EntitySet.ElementType.Name)
            {
                this.dataGridShow.ItemsSource = _myschemaEntities.currency;
            }
            else if (strItemNameGet == _myschemaEntities.exchange.EntitySet.ElementType.Name)
            {
                this.dataGridShow.ItemsSource = _myschemaEntities.exchange;
            }
            else if (strItemNameGet == _myschemaEntities.instrument.EntitySet.ElementType.Name)
            {
                this.dataGridShow.ItemsSource = _myschemaEntities.instrument;
            }
            else if (strItemNameGet == _myschemaEntities.instrument_type.EntitySet.ElementType.Name)
            {
                this.dataGridShow.ItemsSource = _myschemaEntities.instrument_type;
            }
            else if (strItemNameGet == _myschemaEntities.instrument_type_group.EntitySet.ElementType.Name)
            {
                this.dataGridShow.ItemsSource = _myschemaEntities.instrument_type_group;
            }
            else if (strItemNameGet == _myschemaEntities.limit.EntitySet.ElementType.Name)
            {
                this.dataGridShow.ItemsSource = _myschemaEntities.limit;
            }
            else if (strItemNameGet == _myschemaEntities.organisation.EntitySet.ElementType.Name)
            {
                this.dataGridShow.ItemsSource = _myschemaEntities.organisation;
            }
            else if (strItemNameGet == _myschemaEntities.rating.EntitySet.ElementType.Name)
            {
                this.dataGridShow.ItemsSource = _myschemaEntities.rating;
            }
            else if (strItemNameGet == _myschemaEntities.routing_rule.EntitySet.ElementType.Name)
            {
                this.dataGridShow.ItemsSource = _myschemaEntities.routing_rule;
            }
            else if (strItemNameGet == _myschemaEntities.user.EntitySet.ElementType.Name)
            {
                this.dataGridShow.ItemsSource = _myschemaEntities.user;
            }
            else if (strItemNameGet == _myschemaEntities.user_account_relation.EntitySet.ElementType.Name)
            {
                this.dataGridShow.ItemsSource = _myschemaEntities.user_account_relation;
            }
            else if (strItemNameGet == _myschemaEntities.user_instrument_subscription.EntitySet.ElementType.Name)
            {
                this.dataGridShow.ItemsSource = _myschemaEntities.user_instrument_subscription;
            }


			UnitFunClass.SetDataGridReadOnly(this.dataGridShow, this.textboxShowInfo);


        }//treeviewSchemata_Selected






    }// public partial class MainWindow : Window
}//namespace DBManagerMDI






