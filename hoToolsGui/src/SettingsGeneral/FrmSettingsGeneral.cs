﻿using System;
using System.Windows.Forms;
using hoTools.hoToolsGui;
using hoTools.EaServices;
using hoTools.EaServices.WiKiRefs;


// ReSharper disable once CheckNamespace
namespace hoTools.Settings
{
    public partial class FrmSettingsGeneral : Form
    {
        readonly AddinSettings _settings;
        readonly HoToolsGui _hoToolsGui;

        #region Constructor

        /// <summary>
        /// Constructor with
        /// </summary>
        /// <param name="settings">Object with settings</param>
        /// <param name="hoToolsGui"></param>
        public FrmSettingsGeneral(AddinSettings settings, HoToolsGui hoToolsGui)
        {
            InitializeComponent();
            _settings = settings;
            _hoToolsGui = hoToolsGui;
            //this.TopMost = true;

            #region miscellaneous
            // SQL paths
            txtSqlSearchPath.Text = _settings.SqlPaths;
            txtAddinTabToFirstActivate.Text = _settings.AddinTabToFirstActivate;

            chkFeatureOrdering.Checked = _settings.IsOrderFeatures;
            chkReverseEdgeDirection.Checked = settings.IsReverseEdgeDirection;
            chkLineStyleSupport.Checked = settings.IsLineStyleSupport;
            chkShortKeySupport.Checked = settings.IsShortKeySupport;
            _chkQuickSearchSupport.Checked = settings.IsQuickSearchSupport;

            chkShowServiceButtons.Checked = settings.IsShowServiceButton;
            chkShowQueryButtons.Checked = settings.IsShowQueryButton;
            chkFavoriteSupport.Checked = settings.IsFavoriteSupport;
            chkConveyedItemSupport.Checked = settings.IsConveyedItemsSupport;


            txtQuickSearch.Text = settings.QuickSearchName;
            txtFileManagerPath.Text = settings.FileManagerPath;
            chkAdvancedFeatures.Checked = settings.IsAdvancedFeatures;
            chkSvnSupport.Checked = settings.IsSvnSupport;
            chkVcSupport.Checked = settings.IsVcSupport;

            chkAutoCounterSupport.Checked = _settings.IsAutoCounter;

            // Port support
            chkAdvancedPort.Checked = settings.IsAdvancedPort;
            chkPortBasicSupport.Checked = settings.IsPortBasic;
            chkPortTypeSupport.Checked = settings.IsPortType;

            // Block Part synchronization of Ports
            switch (_settings.PartPortSyncho)
            {
                case EaService.PartPortSynchronization.New:
                    _rbPortSynchronizationNew.Checked = true;
                    break;
                case EaService.PartPortSynchronization.Mark:
                    _rbPortSynchronizationMark.Checked = true;
                    break;
                case EaService.PartPortSynchronization.Delete:
                    _rbPortSynchronizationDelete.Checked = true;
                    break;

                default:
                    _rbPortSynchronizationOff.Checked = true;
                    break;
            }
            // LINQPad support
            chkIsLinqPadSupport.Checked = _settings.IsLinqPadSupport;
            txtLinqPadQueryPath.Text = _settings.LinqPadQueryPath;
            txtTempFolder.Text = _settings.TempFolder;
            txtLprunPath.Text = _settings.LprunPath;
            txtLinqPadConnectionXmlFile.Text = _settings.LinqPadConnectionPath;
            chkUseLinqPadConnection.Checked = _settings.UseLinqPadConnection;
            chkOutputLinqPadHtml.Checked = _settings.LinqPadOutputHtml;



            chkAdvancedDiagramNote.Checked = settings.IsAdvancedDiagramNote;
            #endregion

            #region AutoLoadMdg
            // Initialize Auto Load MDG
            switch (_settings.AutoLoadMdgXml)
            {
                case AddinSettings.AutoLoadMdg.Basic:
                    rbAutoLoadMdgBasic.Checked = true;
                    break;
                case AddinSettings.AutoLoadMdg.Compilation:
                    rbAutoLoadMdgCompilation.Checked = true;
                    break;
                default:
                    rbAutoLoadMdgNo.Checked = true;
                    break;
            }
            #endregion

            

            #region LineStyleAndMoreWindow
            // Initialize LineStyle Window
            switch (_settings.LineStyleAndMoreWindow)
            {
                case AddinSettings.ShowInWindow.AddinWindow:
                    rbLineStyleAndMoreAddinWindow.Checked = true;
                    break;
                case AddinSettings.ShowInWindow.TabWindow:
                    rbLineStyleAndMoreTabWindow.Checked = true;
                    break;
                default:
                    rbLineStyleAndMoreDisableWindow.Checked = true;
                    break;
            }
            #endregion

            #region SearchAndReplaceWindow
            // Initialize Search and Replace Window
            switch (_settings.SearchAndReplaceWindow)
            {
                case AddinSettings.ShowInWindow.AddinWindow:
                    rbSearchAndReplaceAddinWindow.Checked = true;
                    break;
                case AddinSettings.ShowInWindow.TabWindow:
                    rbSearchAndReplaceTabWindow.Checked = true;
                    break;
                default:
                    rbSearchAndReplaceDisableWindow.Checked = true;
                    break;
            }
            #endregion
        }
        #endregion

        private void btnOk_Click(object sender, System.EventArgs e)
        {
            try
            {
                // SQL paths
                _settings.SqlPaths = txtSqlSearchPath.Text;
                _settings.AddinTabToFirstActivate = txtAddinTabToFirstActivate.Text;

                _settings.QuickSearchName = txtQuickSearch.Text;
                _settings.FileManagerPath = txtFileManagerPath.Text;

                _settings.IsOrderFeatures = chkFeatureOrdering.Checked;
                _settings.IsReverseEdgeDirection = chkReverseEdgeDirection.Checked;
                _settings.IsLineStyleSupport = chkLineStyleSupport.Checked;
                _settings.IsShortKeySupport = chkShortKeySupport.Checked;
                _settings.IsQuickSearchSupport = _chkQuickSearchSupport.Checked;
                _settings.IsShowServiceButton = chkShowServiceButtons.Checked;
                _settings.IsShowQueryButton = chkShowQueryButtons.Checked;
                _settings.IsFavoriteSupport = chkFavoriteSupport.Checked;
                _settings.IsConveyedItemsSupport = chkConveyedItemSupport.Checked;

                _settings.IsAutoCounter = chkAutoCounterSupport.Checked;

                _settings.IsSvnSupport = chkSvnSupport.Checked;
                _settings.IsVcSupport = chkVcSupport.Checked;
                _settings.IsAdvancedFeatures = chkAdvancedFeatures.Checked;
                // Port
                _settings.IsPortBasic = chkPortBasicSupport.Checked;
                _settings.IsPortType = chkPortTypeSupport.Checked;
                _settings.IsAdvancedPort = chkAdvancedPort.Checked;

                // Block to Part Port synchronization
                _settings.PartPortSyncho = EaService.PartPortSynchronization.Off;
                if (_rbPortSynchronizationDelete.Checked)
                    _settings.PartPortSyncho = EaService.PartPortSynchronization.Delete;
                if (_rbPortSynchronizationMark.Checked)
                    _settings.PartPortSyncho = EaService.PartPortSynchronization.Mark;
                if (_rbPortSynchronizationNew.Checked)
                    _settings.PartPortSyncho = EaService.PartPortSynchronization.New;

                _settings.IsAdvancedDiagramNote = chkAdvancedDiagramNote.Checked;

                #region AutoLoadMdg

                _settings.AutoLoadMdgXml = AddinSettings.AutoLoadMdg.No;
                if (rbAutoLoadMdgBasic.Checked) _settings.AutoLoadMdgXml = AddinSettings.AutoLoadMdg.Basic;
                if (rbAutoLoadMdgCompilation.Checked) _settings.AutoLoadMdgXml = AddinSettings.AutoLoadMdg.Compilation;

                #endregion

                // LINQPad support
                _settings.IsLinqPadSupport = chkIsLinqPadSupport.Checked ;
                _settings.LinqPadQueryPath = txtLinqPadQueryPath.Text;
                _settings.TempFolder = txtTempFolder.Text;
                _settings.LprunPath = txtLprunPath.Text;
                _settings.LinqPadConnectionPath = txtLinqPadConnectionXmlFile.Text;
                _settings.UseLinqPadConnection = chkUseLinqPadConnection.Checked;
                _settings.LinqPadOutputHtml = chkOutputLinqPadHtml.Checked;


                #region LineStyleAndMoreWindow

                _settings.LineStyleAndMoreWindow = AddinSettings.ShowInWindow.Disabled;
                if (rbLineStyleAndMoreAddinWindow.Checked)
                    _settings.LineStyleAndMoreWindow = AddinSettings.ShowInWindow.AddinWindow;
                if (rbLineStyleAndMoreTabWindow.Checked)
                    _settings.LineStyleAndMoreWindow = AddinSettings.ShowInWindow.TabWindow;

                #endregion

                #region SearchAndReplaceWindow

                _settings.SearchAndReplaceWindow = AddinSettings.ShowInWindow.Disabled;
                if (rbSearchAndReplaceAddinWindow.Checked)
                    _settings.SearchAndReplaceWindow = AddinSettings.ShowInWindow.AddinWindow;
                if (rbSearchAndReplaceTabWindow.Checked)
                    _settings.SearchAndReplaceWindow = AddinSettings.ShowInWindow.TabWindow;

                #endregion

                // save setting
                _settings.UpdateGlobalCfg();
                _settings.Save();
                _hoToolsGui.InitializeSettings(); // update settings
                Close();
            }
            catch (Exception e1)

            {
                MessageBox.Show($"{e1}","Can't store settings!");
            }
        }

        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            Close();
        }

        private void label15_Click(object sender, System.EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, System.EventArgs e)
        {

        }

        private void toolStripMenuWiki_Click(object sender, System.EventArgs e)
        {
            WikiRef.Wiki();
        }

        private void generalSettingsToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            WikiRef.WikiSettingsGeneral();
        }

        private void hoToolsToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            WikiRef.WikiHoTools();
        }
        /// <summary>
        /// Ensured that the modal windows is always on top
        /// - On 4K monitors the dialog sometimes get in the background
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmSettingsGeneral_Shown(object sender, System.EventArgs e)
        {
            this.TopMost = true;
        }
    }
}
