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
using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Exceptions;
using ICG.Modules.SimpleFileList.Components;

namespace ICG.Modules.SimpleFileList
{
    public partial class Settings : ModuleSettingsBase
    {

        /// <summary>
        /// handles the loading of the module setting for this
        /// control
        /// </summary>
        public override void LoadSettings()
        {
            try
            {
                if (!IsPostBack)
                {
                    ddlFolder.DataSource = FileUtility.GetAvailableDirectories(Server.MapPath("~/Portals/" + this.PortalId.ToString()));
                    ddlFolder.DataBind();

                    object existing = ModuleSettings["ICG.Modules.SimpleFileList"];
                    if (existing != null)
                    {
                        var item = ddlFolder.Items.FindByText(existing.ToString());
                        if (item != null)
                            item.Selected = true;
                    }

                    object existingExclude = ModuleSettings["ICG.Modules.SimpleFileList.ExcludeFilter"];
                    if (existingExclude != null)
                    {
                        txtExcludeFiles.Text = existingExclude.ToString();
                    }
                    else
                        txtExcludeFiles.Text = ".cs,.vb,.template,.htmtemplate,.resources";

                    object existingSort = ModuleSettings["ICG.Modules.SimpleFIleList.SortOrder"];
                    if (existingSort != null)
                    {
                        ddlSortOrder.SelectedValue = existingSort.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }

        /// <summary>
        /// handles updating the module settings for this control
        /// </summary>
        public override void UpdateSettings()
        {
            try
            {
                var oController = new ModuleController();
                oController.UpdateModuleSetting(this.ModuleId, "ICG.Modules.SimpleFileList", ddlFolder.SelectedItem.Text);
                oController.UpdateModuleSetting(this.ModuleId, "ICG.Modules.SimpleFileList.ExcludeFilter", txtExcludeFiles.Text);
                oController.UpdateModuleSetting(this.ModuleId, "ICG.Modules.SimpleFIleList.SortOrder",
                    ddlSortOrder.SelectedValue);
            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }
    }
}