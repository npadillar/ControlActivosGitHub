<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RptGuia2.aspx.cs" Inherits="App.Web.Logistica.Reportes.RptGuia2" %>

<%@ Import Namespace="Logistica.Libreria.Negocio" %>
<%@ Import Namespace="Logistica.Libreria.Entidad" %>
<%@ Import Namespace="System.Data" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Reporte de guías</title>
    <link rel='shortcut icon' type='image/x-icon' href="Iconos/sise.ico" />
    <link rel="Stylesheet" href="Scripts/estilos.css" />
    <link href="css/bootstrap.css" rel="stylesheet" />
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/3.1.1/jquery.min.js"></script>
    <script type="text/javascript" src="js/bootstrap.min.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            //agregar encabezado
            $('#gvGuia tbody tr:first').wrap('<thead/>').parent().prependTo('#gvGuia');

            $("#gvGuia tbody").on('click', 'tr', function () {
                $("#gvGuia tbody tr").removeClass("resaltar");
                $(this).addClass("resaltar");
            });
        });
    </script>
    <style type="text/css">
        .resaltar {
            background: #acd5f7;
        }

        #gvGuia thead, tbody tr, tfoot tr {
            display: table;
            width: 100%;
            /*el ancho de cada columna permanecerá fijo, del mismo ancho que las columnas del cuerpo*/
            table-layout: fixed;
        }

        #gvGuia thead {
            /*el ancho de la fila del encabezado sera todo menos el ancho del scroll vertical*/
            width: 95%;
            text-align: center;
        }

        #gvGuia {
            width: 95%;
        }

            #gvGuia tbody {
                /*max-height: 50px;*/
                /*mostrar el cuerpo*/
                display: block;
                /*altura del cuerpo*/
                height: 500px;
                /*overflow:auto;*/
                /*mostrar la barra vertical del cuerpo*/
                overflow-y: auto;
                /*ocultar la barra horizontal del cuerpo*/
                overflow-x: hidden;
                text-align: center;
            }

            #gvGuia tr td {
                /*ajustar el contenido al ancho de la columna del cuerpo*/
                word-wrap: break-word;
            }

            #gvGuia thead tr th {
                /*ajustar el contenido al ancho de cada columna del encabezado*/
                word-wrap: break-word;
            }

            #gvGuia thead {
                /*color para el encabezado*/
                background-color: Silver;
            }

            #gvGuia tfoot {
                /*color para el pie*/
                background-color: Silver;
            }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
        <div id="Pagina">
            <div class="headerPrincipal">
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 150px;">
                            <asp:Image ID="imgTitulo" ImageUrl="~/Iconos/logo_black.png" runat="server" />
                        </td>
                        <td>
                            <b>SISTEMA DE CONTROL ACTIVOS</b>
                        </td>
                        <td style="width: 200px; text-align: right;">
                            <b>Usuario:
                        <asp:Label ID="lblUsuario" runat="server" Style="text-align: center" Text="" /></b>
                        </td>
                    </tr>
                </table>
            </div>
            <br />

            <div style="width: 80%; margin: 0 auto;">
                <nav class="navbar navbar-expand-lg navbar-light bg-light" style="border: 2px solid #097dca; border-radius: 5px 5px; background: #fafafa !important;">
                    <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
                        <span class="navbar-toggler-icon"></span>
                    </button>

                    <div class="collapse navbar-collapse" id="navbarSupportedContent">
                        <ul class="navbar-nav mr-auto" style="margin: 0 auto;">
                            <li class="nav-item active">
                                <a class="nav-link" href="https://sistemas.sise.com.pe/sistemas/index.aspx">Menú principal&nbsp;&nbsp;&nbsp;&nbsp;|</a>
                            </li>
                            <%
                                MenuN objMenu = new MenuN();

                                List<MenuEst.EST_MODULO> estMod = new List<MenuEst.EST_MODULO>();
                                List<MenuEst.EST_PAGINA> estPag = new List<MenuEst.EST_PAGINA>();
                                estMod = objMenu.fun_listarModulos_xSistema_xCargo(Convert.ToInt16(Session["cargo"]));

                                foreach (var m in estMod)
                                { %>
                            <li class="nav-item dropdown">
                                <a class="nav-link dropdown-toggle" href="#" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"><%=m.descrip %></a>
                                <div class="dropdown-menu" aria-labelledby="navbarDropdown">
                                    <%
                                        estPag = objMenu.fun_listarPaginas_xModulo_xCargo(m.idMod, Convert.ToInt16(Session["cargo"]));

                                        foreach (var p in estPag)
                                        {
                                    %>
                                    <a class="dropdown-item" href="<%=p.link %>"><%=p.descrip %></a>
                                    <div class="dropdown-divider"></div>
                                    <% }  %>
                                </div>
                            </li>
                            <%  } %>
                            <li class="nav-item active">
                                <a class="nav-link" href="salir.aspx">|&nbsp;&nbsp;&nbsp;&nbsp;Cerrar sesión</a>
                            </li>
                        </ul>
                    </div>
                </nav>
            </div>

            <div class="contentPrincipal">
                <h5 class="alignCenter"><b>REPORTE DE GUÍAS</b></h5>

                <table style="width: 950px;" class="tb">
                    <tr>
                        <th style="width: 150px;">
                            <asp:Button ID="btnBuscar" runat="server" Text="Buscar" OnClick="btnBuscar_Click" CssClass="btn btn-primary" />
                        </th>
                        <td>
                            <asp:Button ID="btnExportar" runat="server" Text="Exportar a excel" OnClick="btnExportar_Click" Visible="false" CssClass="btn btn-primary" />
                        </td>
                        <th style="width: 150px;">
                            Tipo Guía:
                        </th>
                        <td>
                            <asp:DropDownList ID="cboTipoGuia" runat="server" CssClass="cajaTexto">
                                <asp:ListItem Value="99"> --- Todos --- </asp:ListItem>
                                <asp:ListItem Value="1">Activos</asp:ListItem>
                                <asp:ListItem Value="0">Bienes</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <th style="width: 150px;">
                            Fecha desde:
                        </th>
                        <td>
                            <asp:TextBox ID="txtFechaDesde" runat="server" TextMode="Date" CssClass="cajaTexto"></asp:TextBox>
                        </td>
                        <th style="width: 150px;">
                            Fecha hasta:
                        </th>
                        <td>
                            <asp:TextBox ID="txtFechaHasta" runat="server" TextMode="Date" CssClass="cajaTexto"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th style="width: 150px;">
                            Número de Guía:
                        </th>
                        <td>
                            <asp:TextBox ID="txtNumeroGuia" runat="server" CssClass="cajaTexto"></asp:TextBox>
                        </td>
                        <th style="width: 150px;">
                            Código:
                        </th>
                        <td>
                            <asp:TextBox ID="txtCodigo" runat="server" CssClass="cajaTexto"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th style="width: 150px;">
                            Sede de Partida:
                        </th>
                        <td>
                            <asp:DropDownList ID="ddlSedePatida" runat="server" AppendDataBoundItems="true" CssClass="cajaTexto">
                                <asp:ListItem Value="0"> --- Todos --- </asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <th style="width: 150px;">
                            Sede de Llegada:
                        </th>
                        <td>
                            <asp:DropDownList ID="ddlSedeLlegada" runat="server" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlSedeLlegada_SelectedIndexChanged" CssClass="cajaTexto">
                                <asp:ListItem Value="0"> --- Todos --- </asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <th style="width: 150px;">
                            Motivo Traslado:
                        </th>
                        <td>
                            <asp:DropDownList ID="ddlTraslado" runat="server" AppendDataBoundItems="true" CssClass="cajaTexto">
                                <asp:ListItem Value="0"> --- Todos --- </asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <th style="width: 150px;">
                            Estado:
                        </th>
                        <td>
                            <asp:DropDownList ID="ddlEstado" runat="server" AppendDataBoundItems="true" CssClass="cajaTexto">
                                <asp:ListItem Value="0"> --- Todos --- </asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>

            </div>

            <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:GridView ID="gvGuia" DataKeyNames="IdGuia,Activos" runat="server" AutoGenerateColumns="False" BackColor="White" 
                                    BorderColor="#CCCCCC" BorderStyle="None" Font-Size="14px" BorderWidth="1px" CellPadding="4" Style="font-family: Calibri;">
                                    <%--<AlternatingRowStyle BackColor="#CCCCCC" />--%>
                                    <Columns>
                                       <%-- <asp:CommandField SelectText="Detalle" ShowSelectButton="True">
                                            <HeaderStyle Width="50px" />
                                            <ItemStyle Width="50px" />
                                        </asp:CommandField>--%>
                                  <%--      <asp:ButtonField CommandName="seleccionar" HeaderText="VER" Text="Detalle">
                                            <HeaderStyle Width="50px" />
                                            <ItemStyle Width="50px" />
                                        </asp:ButtonField>--%>
                        <asp:HyperLinkField DataNavigateUrlFields="IdGuia,activos,estado,Usuario,Usuario_Recepciona,Motivo_Traslado" DataNavigateUrlFormatString="guiadetalle.aspx?c={0}&a={1}&e={2}&ue={3}&ur={4}&mt={5}" Target="_blank" Text="Detalle">
                            <HeaderStyle Width="50px" />
                            <ItemStyle Width="50px" />
                        </asp:HyperLinkField>
                                        <asp:BoundField DataField="IdGuia" HeaderText="Nro Guía">
                                            <HeaderStyle Width="60px" />
                                            <ItemStyle Width="60px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="detalle" HeaderText="Detalle">
                                            <HeaderStyle Width="200px" />
                                            <ItemStyle Width="200px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Fecha_Envio" HeaderText="Fecha">
                                            <HeaderStyle Width="120px" />
                                            <ItemStyle Width="120px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Usuario" HeaderText="Usuario<br />emisor" HtmlEncode="false">
                                            <HeaderStyle Width="100px" />
                                            <ItemStyle Width="100px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Transportista" HeaderText="Transportista">
                                            <HeaderStyle Width="130px" />
                                            <ItemStyle Width="130px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Punto_Partida" HeaderText="Punto de Partida">
                                            <HeaderStyle Width="100px" />
                                            <ItemStyle Width="100px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Punto_Llegada" HeaderText="Punto de Llegada">
                                            <HeaderStyle Width="120px" />
                                            <ItemStyle Width="120px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Motivo_Traslado" HeaderText="Motivo Traslado">
                                            <HeaderStyle Width="150px" />
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Activos" HeaderText="Act" Visible="False">
                                            <HeaderStyle Width="10px" />
                                            <ItemStyle Width="10px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Usuario_Recepciona" HeaderText="Usuario que Recepciona">
                                            <HeaderStyle Width="100px" />
                                            <ItemStyle Width="100px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Fecha_Recepcion" HeaderText="Fecha Recepcion">
                                            <HeaderStyle Width="120px" />
                                            <ItemStyle Width="120px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Estado" HeaderText="Estado">
                                            <HeaderStyle Width="70px" />
                                            <ItemStyle Width="70px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Usuario_Anulacion" HeaderText="Usuario que Anula la Guia">
                                            <HeaderStyle Width="100px" />
                                            <ItemStyle Width="100px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Fecha_Anulacion" HeaderText="Fecha Anulacion">
                                            <HeaderStyle Width="120px" />
                                            <ItemStyle Width="120px" />
                                        </asp:BoundField>
                                    </Columns>
                                    <FooterStyle BackColor="#CCCCCC" />
                                    <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                    <SortedAscendingHeaderStyle BackColor="#808080" />
                                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                    <SortedDescendingHeaderStyle BackColor="#383838" />
                                </asp:GridView>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </form>
</body>
</html>
