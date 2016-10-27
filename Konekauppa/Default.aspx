<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Konekauppa._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>Konekauppa</h1>
        <p class="lead">Käytetyt kannettavat sinun tarpeisiisi.</p>
        <p><a href="Views/Tuotteet.aspx" class="btn btn-primary btn-lg">Tarkista valikoimamme &raquo;</a></p>
    </div>

    <div class="row">
        <div class="col-md-4">
            <h2>Ota yhteyttä</h2>
            <p>
                Ota yhteyttä ja tiedustele, löytyisikö meiltä sinulle sopiva kone!           
            </p>
            <p>
                <a class="btn btn-default" href="Contact.aspx">Learn more &raquo;</a>
            </p>
        </div>
    </div>

</asp:Content>
