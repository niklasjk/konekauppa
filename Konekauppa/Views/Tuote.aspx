<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Tuote.aspx.cs" Inherits="Konekauppa.Views.Tuote" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:FormView ID="FormView1" runat="server" DataKeyNames="TuoteID" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical">
        <EditItemTemplate>
            TuoteID:
            <asp:Label ID="TuoteIDLabel1" runat="server" Text='<%# Eval("TuoteID") %>' />
            <br />
            Nimi:
            <asp:TextBox ID="NimiTextBox" runat="server" Text='<%# Bind("Nimi") %>' />
            <br />
            Hinta:
            <asp:TextBox ID="HintaTextBox" runat="server" Text='<%# Bind("Hinta") %>' />
            <br />
            Veroprosentti:
            <asp:TextBox ID="VeroprosenttiTextBox" runat="server" Text='<%# Bind("Veroprosentti") %>' />
            <br />
            <asp:LinkButton ID="UpdateButton" runat="server" CausesValidation="True" CommandName="Update" Text="Update" />
            &nbsp;<asp:LinkButton ID="UpdateCancelButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" />
        </EditItemTemplate>
        <EditRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
        <FooterStyle BackColor="#CCCCCC" />
        <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
        <InsertItemTemplate>
            TuoteID:
            <asp:TextBox ID="TuoteIDTextBox" runat="server" Text='<%# Bind("TuoteID") %>' />
            <br />
            Nimi:
            <asp:TextBox ID="NimiTextBox" runat="server" Text='<%# Bind("Nimi") %>' />
            <br />
            Hinta:
            <asp:TextBox ID="HintaTextBox" runat="server" Text='<%# Bind("Hinta") %>' />
            <br />
            Veroprosentti:
            <asp:TextBox ID="VeroprosenttiTextBox" runat="server" Text='<%# Bind("Veroprosentti") %>' />
            <br />
            <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert" Text="Insert" />
            &nbsp;<asp:LinkButton ID="InsertCancelButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" />
        </InsertItemTemplate>
        <ItemTemplate>
            TuoteID:
            <asp:Label ID="TuoteIDLabel" runat="server" Text='<%# Eval("TuoteID") %>' />
            <br />
            Nimi:
            <asp:Label ID="NimiLabel" runat="server" Text='<%# Bind("Nimi") %>' />
            <br />
            Hinta:
            <asp:Label ID="HintaLabel" runat="server" Text='<%# Bind("Hinta") %>' />
            <br />
            Veroprosentti:
            <asp:Label ID="VeroprosenttiLabel" runat="server" Text='<%# Bind("Veroprosentti") %>' />
            <br />

        </ItemTemplate>
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
    </asp:FormView>

    <asp:Label ID="Label1" runat="server" Text="Lukumäärä: "></asp:Label><asp:TextBox ID="TextBoxLukumaara" runat="server"></asp:TextBox><br />
    <asp:Label ID="Label2" runat="server" Text="Maksuehto: "></asp:Label><asp:DropDownList ID="DropDownListMaksuehto" runat="server" DataSourceID="SqlDataSource1" DataTextField="Maksuehto" DataValueField="Maksuehto"></asp:DropDownList>
   <%-- <asp:LoginView ID="LoginView1" runat="server">
        <AnonymousTemplate>
    <asp:Label ID="Label3" runat="server" Text="Etunimi: "></asp:Label><asp:TextBox ID="TextBoxEtunimi" runat="server"></asp:TextBox><br />
    <asp:Label ID="Label4" runat="server" Text="Sukunimi: "></asp:Label><asp:TextBox ID="sukunimi" runat="server"></asp:TextBox><br />

        </AnonymousTemplate>
    </asp:LoginView>--%>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT * FROM [Maksuehto]"></asp:SqlDataSource>
        
    <br />
    <asp:Button ID="ButtonTilaa" runat="server" Text="Tilaa" OnClick="ButtonTilaa_Click" />
    <br />
    <asp:Label ID="LabelOnnistuikoTilaus" runat="server" Text="Status"></asp:Label>

</asp:Content>
