using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

/// <summary>
/// Descripción breve de Comunes
/// </summary>
public class Comunes
{ 
    public Comunes()
    {
    }

    //Carga en un DropDownList (primer parámetro), los datos que están en un dataset
    //(segundo parámetro), dentro de la tabla y columna dadas (tercer y cuarto parámetros).
    public void cargaDDL(DropDownList ddl, DataSet ds, String tabla, String col)
    {
        DataTable tabResul;

        ddl.Items.Clear();
        //La columna 'col' es la que tiene los datos que se agregarán al DDL.
        tabResul = ds.Tables[tabla];
        ddl.Items.Add(" ");
        foreach (DataRow fila in tabResul.Rows)
            ddl.Items.Add(fila[col].ToString());
    }

}