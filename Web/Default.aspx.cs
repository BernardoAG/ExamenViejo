using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    string cadSql;
    DataSet dsDist = new DataSet();
    DataSet dsPed = new DataSet();
    DataSet dsPed1 = new DataSet();
    DataSet dsEd = new DataSet();
    DataSet dsPedidoLibro = new DataSet();
    Comunes comunes = new Comunes();
    const int OK = 1;
    GestorBD.GestorBD GestorBD;         //Para manejar la BD.


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GestorBD = new GestorBD.GestorBD("Microsoft.ACE.OLEDB.12.0", "Admin", "",
                          "C:/Users/BALTAMIRG/ExamenViejo/BD2oPedLibros.accdb");
            Session["GestorBD"] = GestorBD;
            cadSql = "select distinct nomEd from Editorial";
            GestorBD.consBD(cadSql, dsEd, "Editorial");
            comunes.cargaDDL(DropDownList1, dsEd, "Editorial", "nomEd");
            Session["DsGeneral"] = dsEd;
        }
        
    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        GestorBD = (GestorBD.GestorBD)Session["GestorBD"];
        cadSql = "select distinct nomDis from Editorial e, Distribuidor d, distribuye di where nomEd = '"+DropDownList1.SelectedValue+"' and e.idEd = di.idEd and di.rfcDis = d.rfcDis";
        GestorBD.consBD(cadSql, dsDist, "Dist");
        comunes.cargaDDL(DropDownList2, dsDist, "Dist", "nomDis");
        Session["dsDist"] = dsDist;
    }



    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        string rfc;

        GestorBD = (GestorBD.GestorBD)Session["GestorBD"];
        cadSql = "select idPed from Distribuidor d, pedido p where nomDis='" + DropDownList2.SelectedValue + "' and d.rfcDis = p.rfcDis ";
        Response.Write(cadSql);
        GestorBD.consBD(cadSql, dsPed, "Ped");
        comunes.cargaDDL(DropDownList3, dsPed, "Ped", "idPed");
        Session["dsPed"] = dsPed;
    }
}