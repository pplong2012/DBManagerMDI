
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
using System.Data.Metadata.Edm;

using DBManagerMDI.Model;


namespace DBManagerMDI.ViewModel
{
	class UnitFunClass
	{

		public static void SetDataGridReadOnly(DataGrid dataGridShowWin, TextBox showWin)
		{
			System.String strShowInfo = System.String.Empty;

			//use cann't edit dataGrid
			dataGridShowWin.IsReadOnly = true;
			strShowInfo = "User Cann't Modify, AddRows or DeleteRows Now";
			ShowMsgInInfoWindow(showWin, LogLevelEnum.LOG_WARN, strShowInfo);
		}
		public static void ResetInfoWindow(TextBox showWin)
		{
			showWin.Text = "";
		}
		public static void ShowMsgInInfoWindow(TextBox showWin, LogLevelEnum nMsgType, System.String strMsg)
		{
			System.String strOldTest = System.String.Empty;

			System.String strShowInfo = System.String.Empty;
			//System.DateTime nTimeNow = System.DateTime.Now;
			System.String strMsgType = System.String.Empty;

			strOldTest = showWin.Text;

			switch (nMsgType)
			{
				case LogLevelEnum.LOG_WARN:
					strMsgType = " WARN: ";
					break;
				case LogLevelEnum.LOG_INFO:
					strMsgType = " INFO: ";
					break;
				case LogLevelEnum.LOG_ERROR:
					strMsgType = " ERROR: ";
					break;
				default:
					strMsgType = " INFO: ";
					break;
			}//switch

			strShowInfo = strOldTest;
			//add Msg to tail
			//Millisecond
			strShowInfo += System.DateTime.Now.ToString("\n yyyy-MM-dd HH:mm:ss.fff -- ");
			//strShowInfo = System.DateTime.Now.Millisecond.ToString();
			strShowInfo += strMsgType;
			strShowInfo += strMsg;

			showWin.Text = strShowInfo;

		}
		public static void ShowInfoRowAffected(DataGrid dataGridShowWin, TextBox showWin, System.Int32 nRowAffected)
		{
			System.String strShowInfo = System.String.Empty;

			strShowInfo = "  " + nRowAffected.ToString() + " row affected by the last command, no resultset returned";

			ResetInfoWindow(showWin);
			ShowMsgInInfoWindow(showWin, LogLevelEnum.LOG_ERROR, strShowInfo);
			SetDataGridReadOnly(dataGridShowWin, showWin);

		}
		public static void ShowInfoError(TextBox showWin, System.String strErrorInfo)
		{
			System.String strShowInfo = System.String.Empty;

			strShowInfo = "ErrorInfo: " + strErrorInfo;
			ResetInfoWindow(showWin);
			ShowMsgInInfoWindow(showWin, LogLevelEnum.LOG_ERROR, strShowInfo);
		}

		public static void ShowInfoReSet(TextBox showWin, System.String strInfo)
		{
			System.String strShowInfo = System.String.Empty;

			ResetInfoWindow(showWin);
			strShowInfo = strInfo;
			UnitFunClass.ShowMsgInInfoWindow(showWin, LogLevelEnum.LOG_INFO, strShowInfo);

		}

		public static void ShowInfoAppend(TextBox showWin, System.String strInfo)
		{
			System.String strShowInfo = System.String.Empty;

			strShowInfo = strInfo;
			UnitFunClass.ShowMsgInInfoWindow(showWin, LogLevelEnum.LOG_INFO, strShowInfo);

		}

	}
}
