<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Settings.ascx.cs" Inherits="ICG.Modules.SimpleFileList.Settings" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>

<div class="dnnForm">
    <div class="dnnFormItem">
        <dnn:label id="lblFolderToList" runat="server" suffix=":" controlname="ddlFolder" cssclass="dnnFormRequired" />
        <asp:DropDownList ID="ddlFolder" runat="server" />
    </div>
    <div class="dnnFormItem">
        <dnn:label id="lblExcludeFiles" runat="server" suffix=":" controlname="txtExcludeFiles" />
        <asp:TextBox ID="txtExcludeFiles" runat="server" Width="300px" TextMode="MultiLine" />
    </div>
    <div class="dnnFormItem">
        <dnn:Label id="lblSortOrder" runat="server" suffix=":" />
        <asp:DropDownList runat="server" ID="ddlSortOrder">
            <asp:ListItem Value="FA" Text="File Name (A - Z)" />
            <asp:ListItem Value="FD" Text="File Name (Z - A)" />
            <asp:ListItem Value="DA" Text="Date (Old - New)" />
            <asp:ListItem Value="DD" Text="Date (New - Old)" />
        </asp:DropDownList>
    </div>
</div>
