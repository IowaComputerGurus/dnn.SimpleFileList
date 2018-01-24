<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="View.ascx.cs" Inherits="ICG.Modules.SimpleFileList.View" %>

<asp:DataGrid ID="dgrFileList" runat="server" AutoGenerateColumns="false" CssClass="ICGDataGrid" 
    onitemdatabound="DgrFileList_ItemDataBound">
    <HeaderStyle CssClass="ICGDataGridHeader" />
    <ItemStyle CssClass="ICGDataGridItem" />
    <AlternatingItemStyle CssClass="ICGDataGridAltItem" />
    <Columns>
        <asp:BoundColumn DataField="FileName" />
        <asp:TemplateColumn>
            <ItemTemplate>
                <asp:HyperLink ID="hlDownload" runat="server" />
            </ItemTemplate>
        </asp:TemplateColumn>
    </Columns>
</asp:DataGrid>