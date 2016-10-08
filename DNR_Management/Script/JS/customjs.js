// JavaScript Document

$(function(){
	   
	  
	$("#export_excel_button").click(function () {
	    $("#tableThousandList").btechco_excelexport({
	        containerid: "tableThousandList"
           , datatype: $datatype.Table
        });
	});

	$("#btn-Excell-Mathugama").click(function () {
	    $("#table_MathugamaExcel").btechco_excelexport({
	        containerid: "table_MathugamaExcel"
            , datatype: $datatype.Table
	    });
	    $('#btn-Save-Mathugama').show();
	});
	
	$("#btn-Excell-fullerton").click(function () {
	    $("#table_fullertonExcel").btechco_excelexport({
	        containerid: "table_fullertonExcel"
            , datatype: $datatype.Table
	    });
	    $('#btn-Save-fullerton').show();

	});

	$("#btn-Excell-Agalawatta").click(function () {
        debugger
	    $("#table_AgalawattaExcel").btechco_excelexport({
	        containerid: "table_AgalawattaExcel"
            , datatype: $datatype.Table
	    });
	    $('#btn-Save-Agalawatta').show();
	    
	});

	$("#btn-Excell-Beruwala").click(function () {
	    $("#table_BeruwalaExcel").btechco_excelexport({
	        containerid: "table_BeruwalaExcel"
            , datatype: $datatype.Table
	    });
	    $('#btn-Save-Beruwala').show();
	});

	$("#btn-Excell-Panadura").click(function () {
	    $("#table_PanaduraExcell").btechco_excelexport({
	        containerid: "table_PanaduraExcell"
            , datatype: $datatype.Table
	    });
	    $('#btn-Save-Panadura').show();
	});

	$("#export_excel_OrderCard_button").click(function () {
	    debugger;
	    $("#table_OrerCardList").btechco_excelexport({
	        containerid: "table_OrerCardList"
            , datatype: $datatype.Table
	    });
	    $('#btn_OrderCard_remove').show();
	});
	
	});