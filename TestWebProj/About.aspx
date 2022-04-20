<%@ Page Title="About Us" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="About.aspx.cs" Inherits="TestWebProj.About" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <table border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                CrDr
            </td>
            <td>
                <asp:DropDownList ID="ddlCrDr" runat="server" >
                    <asp:ListItem Text="Deposit" Value="Cr" />
                    <asp:ListItem Text="Withdraw" Value="Dr" />
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                Account
            </td>
         
            <td>
                <asp:TextBox ID="txtFromAcc" runat="server"></asp:TextBox>
            </td>
            <td>
                <span>|</span>
            </td>
            <td>
                Type
            </td>
            <td>
                <asp:TextBox ID="txtType" runat="server"></asp:TextBox>
            </td>
        </tr>
<%--
        <tr>
            <td>
                To Account
            </td>
            <td>
                <asp:DropDownList ID="ddlToAccount" runat="server">
                    <asp:ListItem Text="Person" Value="Person" />
                    <asp:ListItem Text="Bank" Value="Bank" />
                    <asp:ListItem Text="Other Bank" Value="OtherBank" />
                </asp:DropDownList>
            </td>
            <td>
                <span>|</span>
            </td>
            <td>
                <asp:TextBox ID="txtToAcc" runat="server"></asp:TextBox>
            </td>
        </tr>--%>

        <tr>
            <td>
                Amount
            </td>
            <td>
                <asp:TextBox ID="txtAmount" runat="server"></asp:TextBox>
            </td>
            <td>
                <span>|</span>
            </td>
            <td>
                Date
            </td>
            <td>
                <asp:TextBox ID="txtDate" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnTransaction" runat="server" Text="Transaction" OnClick="btnTransaction_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
