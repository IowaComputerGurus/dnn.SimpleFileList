/*
 *  Copyright (c) 2006 - 2011 IowaComputerGurus Inc, All rights reserved.
 *  
 *  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED
 *  TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
 *  THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF
 *  CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
 *  DEALINGS IN THE SOFTWARE.
 */

using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Services.Localization;
using DotNetNuke.UI.Skins;
using DotNetNuke.UI.Skins.Controls;
using ICG.Modules.SimpleFileList.Components;

namespace ICG.Modules.SimpleFileList
{
    public partial class View : PortalModuleBase
    {
        /// <summary>
        /// Holds the root folder path, used for binding operations
        /// </summary>
        private string _rootPath;

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    _rootPath = Server.MapPath("~/");
                    var settingValue = Settings["ICG.Modules.SimpleFileList"];
                    if (settingValue != null)
                    {
                        var portalPath = Server.MapPath("~/Portals/" + this.PortalId);

                        dgrFileList.Columns[0].HeaderText = Localization.GetString("FileHeader", this.LocalResourceFile);
                        dgrFileList.DataSource = FileUtility.GetSafeFileList(string.Concat(portalPath, settingValue.ToString()), GetExcludedFiles(), GetSortOrder());
                        dgrFileList.DataBind();
                    }
                    else
                    {
                        Skin.AddModuleMessage(this, Localization.GetString("NotConfigured", this.LocalResourceFile), ModuleMessage.ModuleMessageType.BlueInfo);
                    }

                }
            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }

        #region Display Binding Values
        /// <summary>
        /// Handles the ItemDataBound event of the dgrFileList control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.DataGridItemEventArgs"/> instance containing the event data.</param>
        protected void dgrFileList_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var hlDownload = (HyperLink)e.Item.FindControl("hlDownload");
                var oItem = (FileDisplay)e.Item.DataItem;
                string filePath = oItem.FullPath.Replace(_rootPath, @"\");
                hlDownload.NavigateUrl = filePath.Replace(@"\", "/");
                hlDownload.Text = Localization.GetString("DownloadLink", this.LocalResourceFile);
                hlDownload.Target = "_blank";
            }
        }
        #endregion

        #region Settings Helpers
        /// <summary>
        /// Gets the excluded files.
        /// </summary>
        /// <returns></returns>
        private IEnumerable<string> GetExcludedFiles()
        {
            var settingValue = Settings["ICG.Modules.SimpleFileList.ExcludeFilter"];
            if (settingValue == null)
            {
                //Force default value in
                var modController = new ModuleController();
                var defaultFilter = ".cs,.vb,.template,.htmtemplate,.resources";
                modController.UpdateModuleSetting(this.ModuleId, "ICG.Modules.SimpleFileList.ExcludeFilter", defaultFilter);
                settingValue = defaultFilter;
            }
            var finalSetting = settingValue.ToString();
            return finalSetting.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries);
        }

        /// <summary>
        /// Gets the sort order.
        /// </summary>
        /// <returns>System.String.</returns>
        private string GetSortOrder()
        {
            var value = Settings["ICG.Modules.SimpleFIleList.SortOrder"];
            return value != null ? value.ToString() : "FA";
        }

        #endregion

    }
}