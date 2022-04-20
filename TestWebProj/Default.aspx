<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="TestWebProj._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <table border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                Principal Amt
            </td>
            <td>
                <asp:TextBox ID="txtPrincipal" runat="server"></asp:TextBox>
            </td>
            <td><span>|</span></td>
            <td>
                Loan Account No
            </td>
            <td>
                <asp:TextBox ID="txtLoanAcc" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Tenor(In Month)
            </td>
            <td>
                <asp:TextBox ID="txtTenor" runat="server"></asp:TextBox>
            </td>
            <td><span>|</span></td>
            <td>
               Grace Period(in Month)
            </td>
            <td>
                <asp:TextBox ID="txtGracePeriod" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Interest Rate(%)
            </td>
            <td>
                <asp:TextBox ID="txtInterestRate" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="Button1" runat="server" Text="Calculate" 
                    onclick="Button1_Click" />
            </td>
        </tr>
    </table>
    <asp:GridView ID="GridView1" runat="server">
    </asp:GridView>

    <asp:Button ID="btnDisburse" runat="server" Text="Disburse" Visible=false 
        onclick="btnDisburse_Click" />
</asp:Content>
