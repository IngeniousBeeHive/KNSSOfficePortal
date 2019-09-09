<%@ Page Title="" Language="C#" MasterPageFile="~/Reports/ReportMaster.Master" AutoEventWireup="true" CodeBehind="PettyCash.aspx.cs" Inherits="office.hmvtrust.com.Reports.MasterReports.PettyCash" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, 
PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server" >

   <%--     <div class="row" style="margin:0px,0px">
            <h4 class="col-xs-8"><i class="fa fa-line-chart page-header-icon"></i>&nbsp;&nbsp;Reports&nbsp;&nbsp;/&nbsp;&nbsp;Branches</h4>
        </div>--%>


    <ul class="breadcrumb breadcrumb-page">
        <div class="breadcrumb-label text-light-gray">You are here: </div>
            <li><a href="/Reports/Index">Reports</a></li>
            <li class="active"><a href="#">Branches</a></li>
    </ul>
    


<rsweb:ReportViewer ID="ReportViewer1" CssClass="panel"
         runat="server" Font-Names="Verdana" Font-Size="8pt"  ShowParameterPrompts="true"
        WaitMessageFont-Names="Verdana" Height="600px" WaitMessageFont-Size="14pt" Width="100%" >

    <LocalReport ReportPath="Reports\RDLC\rptPettyCash.rdl" EnableExternalImages="true">
    </LocalReport>

</rsweb:ReportViewer>
</asp:Content>
