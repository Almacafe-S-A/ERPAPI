﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

using System.Xml.Serialization;

// 
// Este código fuente fue generado automáticamente por xsd, Versión=4.6.1055.0.
// 


/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace="", IsNullable=false)]
public partial class CONSOLIDATED_LIST {
    
    private INDIVIDUAL[] iNDIVIDUALSField;
    
    private ENTITY[] eNTITIESField;
    
    private System.DateTime dateGeneratedField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("INDIVIDUAL", IsNullable=false)]
    public INDIVIDUAL[] INDIVIDUALS {
        get {
            return this.iNDIVIDUALSField;
        }
        set {
            this.iNDIVIDUALSField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("ENTITY", IsNullable=false)]
    public ENTITY[] ENTITIES {
        get {
            return this.eNTITIESField;
        }
        set {
            this.eNTITIESField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public System.DateTime dateGenerated {
        get {
            return this.dateGeneratedField;
        }
        set {
            this.dateGeneratedField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace="", IsNullable=false)]
public partial class INDIVIDUAL {
    
    private string dATAIDField;
    
    private string vERSIONNUMField;
    
    private string fIRST_NAMEField;
    
    private string sECOND_NAMEField;
    
    private string tHIRD_NAMEField;
    
    private string fOURTH_NAMEField;
    
    private string uN_LIST_TYPEField;
    
    private string rEFERENCE_NUMBERField;
    
    private System.DateTime lISTED_ONField;
    
    private string gENDERField;
    
    private string sUBMITTED_BYField;
    
    private string nAME_ORIGINAL_SCRIPTField;
    
    private string cOMMENTS1Field;
    
    private string nATIONALITY2Field;
    
    private string[] tITLEField;
    
    private string[] dESIGNATIONField;
    
    private string[] nATIONALITYField;
    
    private LIST_TYPE lIST_TYPEField;
    
    private string[] lAST_DAY_UPDATEDField;
    
    private INDIVIDUAL_ALIAS[] iNDIVIDUAL_ALIASField;
    
    private INDIVIDUAL_ADDRESS[] iNDIVIDUAL_ADDRESSField;
    
    private INDIVIDUAL_DATE_OF_BIRTH[] iNDIVIDUAL_DATE_OF_BIRTHField;
    
    private INDIVIDUAL_PLACE_OF_BIRTH[] iNDIVIDUAL_PLACE_OF_BIRTHField;
    
    private INDIVIDUAL_DOCUMENT[] iNDIVIDUAL_DOCUMENTField;
    
    private string sORT_KEYField;
    
    private string sORT_KEY_LAST_MODField;
    
    private System.DateTime dELISTED_ONField;
    
    private bool dELISTED_ONFieldSpecified;
    
    public INDIVIDUAL() {
        this.dATAIDField = "000000";
        this.vERSIONNUMField = "1";
        this.sECOND_NAMEField = "na";
        this.tHIRD_NAMEField = "na";
        this.fOURTH_NAMEField = "na";
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(DataType="integer")]
    public string DATAID {
        get {
            return this.dATAIDField;
        }
        set {
            this.dATAIDField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(DataType="integer")]
    public string VERSIONNUM {
        get {
            return this.vERSIONNUMField;
        }
        set {
            this.vERSIONNUMField = value;
        }
    }
    
    /// <remarks/>
    public string FIRST_NAME {
        get {
            return this.fIRST_NAMEField;
        }
        set {
            this.fIRST_NAMEField = value;
        }
    }
    
    /// <remarks/>
    public string SECOND_NAME {
        get {
            return this.sECOND_NAMEField;
        }
        set {
            this.sECOND_NAMEField = value;
        }
    }
    
    /// <remarks/>
    public string THIRD_NAME {
        get {
            return this.tHIRD_NAMEField;
        }
        set {
            this.tHIRD_NAMEField = value;
        }
    }
    
    /// <remarks/>
    public string FOURTH_NAME {
        get {
            return this.fOURTH_NAMEField;
        }
        set {
            this.fOURTH_NAMEField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(DataType="NCName")]
    public string UN_LIST_TYPE {
        get {
            return this.uN_LIST_TYPEField;
        }
        set {
            this.uN_LIST_TYPEField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(DataType="NCName")]
    public string REFERENCE_NUMBER {
        get {
            return this.rEFERENCE_NUMBERField;
        }
        set {
            this.rEFERENCE_NUMBERField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(DataType="date")]
    public System.DateTime LISTED_ON {
        get {
            return this.lISTED_ONField;
        }
        set {
            this.lISTED_ONField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(DataType="NCName")]
    public string GENDER {
        get {
            return this.gENDERField;
        }
        set {
            this.gENDERField = value;
        }
    }
    
    /// <remarks/>
    public string SUBMITTED_BY {
        get {
            return this.sUBMITTED_BYField;
        }
        set {
            this.sUBMITTED_BYField = value;
        }
    }
    
    /// <remarks/>
    public string NAME_ORIGINAL_SCRIPT {
        get {
            return this.nAME_ORIGINAL_SCRIPTField;
        }
        set {
            this.nAME_ORIGINAL_SCRIPTField = value;
        }
    }
    
    /// <remarks/>
    public string COMMENTS1 {
        get {
            return this.cOMMENTS1Field;
        }
        set {
            this.cOMMENTS1Field = value;
        }
    }
    
    /// <remarks/>
    public string NATIONALITY2 {
        get {
            return this.nATIONALITY2Field;
        }
        set {
            this.nATIONALITY2Field = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("VALUE", IsNullable=false)]
    public string[] TITLE {
        get {
            return this.tITLEField;
        }
        set {
            this.tITLEField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("VALUE", IsNullable=false)]
    public string[] DESIGNATION {
        get {
            return this.dESIGNATIONField;
        }
        set {
            this.dESIGNATIONField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("VALUE", IsNullable=false)]
    public string[] NATIONALITY {
        get {
            return this.nATIONALITYField;
        }
        set {
            this.nATIONALITYField = value;
        }
    }
    
    /// <remarks/>
    public LIST_TYPE LIST_TYPE {
        get {
            return this.lIST_TYPEField;
        }
        set {
            this.lIST_TYPEField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("VALUE", IsNullable=false)]
    public string[] LAST_DAY_UPDATED {
        get {
            return this.lAST_DAY_UPDATEDField;
        }
        set {
            this.lAST_DAY_UPDATEDField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("INDIVIDUAL_ALIAS")]
    public INDIVIDUAL_ALIAS[] INDIVIDUAL_ALIAS {
        get {
            return this.iNDIVIDUAL_ALIASField;
        }
        set {
            this.iNDIVIDUAL_ALIASField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("INDIVIDUAL_ADDRESS")]
    public INDIVIDUAL_ADDRESS[] INDIVIDUAL_ADDRESS {
        get {
            return this.iNDIVIDUAL_ADDRESSField;
        }
        set {
            this.iNDIVIDUAL_ADDRESSField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("INDIVIDUAL_DATE_OF_BIRTH")]
    public INDIVIDUAL_DATE_OF_BIRTH[] INDIVIDUAL_DATE_OF_BIRTH {
        get {
            return this.iNDIVIDUAL_DATE_OF_BIRTHField;
        }
        set {
            this.iNDIVIDUAL_DATE_OF_BIRTHField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("INDIVIDUAL_PLACE_OF_BIRTH")]
    public INDIVIDUAL_PLACE_OF_BIRTH[] INDIVIDUAL_PLACE_OF_BIRTH {
        get {
            return this.iNDIVIDUAL_PLACE_OF_BIRTHField;
        }
        set {
            this.iNDIVIDUAL_PLACE_OF_BIRTHField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("INDIVIDUAL_DOCUMENT")]
    public INDIVIDUAL_DOCUMENT[] INDIVIDUAL_DOCUMENT {
        get {
            return this.iNDIVIDUAL_DOCUMENTField;
        }
        set {
            this.iNDIVIDUAL_DOCUMENTField = value;
        }
    }
    
    /// <remarks/>
    public string SORT_KEY {
        get {
            return this.sORT_KEYField;
        }
        set {
            this.sORT_KEYField = value;
        }
    }
    
    /// <remarks/>
    public string SORT_KEY_LAST_MOD {
        get {
            return this.sORT_KEY_LAST_MODField;
        }
        set {
            this.sORT_KEY_LAST_MODField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(DataType="date")]
    public System.DateTime DELISTED_ON {
        get {
            return this.dELISTED_ONField;
        }
        set {
            this.dELISTED_ONField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool DELISTED_ONSpecified {
        get {
            return this.dELISTED_ONFieldSpecified;
        }
        set {
            this.dELISTED_ONFieldSpecified = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace="", IsNullable=false)]
public partial class LIST_TYPE {
    
    private string vALUEField;
    
    /// <remarks/>
    public string VALUE {
        get {
            return this.vALUEField;
        }
        set {
            this.vALUEField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace="", IsNullable=false)]
public partial class INDIVIDUAL_ALIAS {
    
    private string qUALITYField;
    
    private string aLIAS_NAMEField;
    
    private string dATE_OF_BIRTHField;
    
    private string cITY_OF_BIRTHField;
    
    private string cOUNTRY_OF_BIRTHField;
    
    private string nOTEField;
    
    /// <remarks/>
    public string QUALITY {
        get {
            return this.qUALITYField;
        }
        set {
            this.qUALITYField = value;
        }
    }
    
    /// <remarks/>
    public string ALIAS_NAME {
        get {
            return this.aLIAS_NAMEField;
        }
        set {
            this.aLIAS_NAMEField = value;
        }
    }
    
    /// <remarks/>
    public string DATE_OF_BIRTH {
        get {
            return this.dATE_OF_BIRTHField;
        }
        set {
            this.dATE_OF_BIRTHField = value;
        }
    }
    
    /// <remarks/>
    public string CITY_OF_BIRTH {
        get {
            return this.cITY_OF_BIRTHField;
        }
        set {
            this.cITY_OF_BIRTHField = value;
        }
    }
    
    /// <remarks/>
    public string COUNTRY_OF_BIRTH {
        get {
            return this.cOUNTRY_OF_BIRTHField;
        }
        set {
            this.cOUNTRY_OF_BIRTHField = value;
        }
    }
    
    /// <remarks/>
    public string NOTE {
        get {
            return this.nOTEField;
        }
        set {
            this.nOTEField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace="", IsNullable=false)]
public partial class INDIVIDUAL_ADDRESS {
    
    private string sTREETField;
    
    private string cITYField;
    
    private string sTATE_PROVINCEField;
    
    private string zIP_CODEField;
    
    private string cOUNTRYField;
    
    private string nOTEField;
    
    /// <remarks/>
    public string STREET {
        get {
            return this.sTREETField;
        }
        set {
            this.sTREETField = value;
        }
    }
    
    /// <remarks/>
    public string CITY {
        get {
            return this.cITYField;
        }
        set {
            this.cITYField = value;
        }
    }
    
    /// <remarks/>
    public string STATE_PROVINCE {
        get {
            return this.sTATE_PROVINCEField;
        }
        set {
            this.sTATE_PROVINCEField = value;
        }
    }
    
    /// <remarks/>
    public string ZIP_CODE {
        get {
            return this.zIP_CODEField;
        }
        set {
            this.zIP_CODEField = value;
        }
    }
    
    /// <remarks/>
    public string COUNTRY {
        get {
            return this.cOUNTRYField;
        }
        set {
            this.cOUNTRYField = value;
        }
    }
    
    /// <remarks/>
    public string NOTE {
        get {
            return this.nOTEField;
        }
        set {
            this.nOTEField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace="", IsNullable=false)]
public partial class INDIVIDUAL_DATE_OF_BIRTH {
    
    private string tYPE_OF_DATEField;
    
    private string nOTEField;
    
    private object[] itemsField;
    
    private ItemsChoiceType[] itemsElementNameField;
    
    /// <remarks/>
    public string TYPE_OF_DATE {
        get {
            return this.tYPE_OF_DATEField;
        }
        set {
            this.tYPE_OF_DATEField = value;
        }
    }
    
    /// <remarks/>
    public string NOTE {
        get {
            return this.nOTEField;
        }
        set {
            this.nOTEField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("DATE", typeof(System.DateTime), DataType="date")]
    [System.Xml.Serialization.XmlElementAttribute("FROM_YEAR", typeof(string), DataType="integer")]
    [System.Xml.Serialization.XmlElementAttribute("TO_YEAR", typeof(string), DataType="integer")]
    [System.Xml.Serialization.XmlElementAttribute("YEAR", typeof(string), DataType="integer")]
    [System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")]
    public object[] Items {
        get {
            return this.itemsField;
        }
        set {
            this.itemsField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("ItemsElementName")]
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public ItemsChoiceType[] ItemsElementName {
        get {
            return this.itemsElementNameField;
        }
        set {
            this.itemsElementNameField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
[System.SerializableAttribute()]
[System.Xml.Serialization.XmlTypeAttribute(IncludeInSchema=false)]
public enum ItemsChoiceType {
    
    /// <remarks/>
    DATE,
    
    /// <remarks/>
    FROM_YEAR,
    
    /// <remarks/>
    TO_YEAR,
    
    /// <remarks/>
    YEAR,
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace="", IsNullable=false)]
public partial class INDIVIDUAL_PLACE_OF_BIRTH {
    
    private string cITYField;
    
    private string sTATE_PROVINCEField;
    
    private string cOUNTRYField;
    
    private string nOTEField;
    
    /// <remarks/>
    public string CITY {
        get {
            return this.cITYField;
        }
        set {
            this.cITYField = value;
        }
    }
    
    /// <remarks/>
    public string STATE_PROVINCE {
        get {
            return this.sTATE_PROVINCEField;
        }
        set {
            this.sTATE_PROVINCEField = value;
        }
    }
    
    /// <remarks/>
    public string COUNTRY {
        get {
            return this.cOUNTRYField;
        }
        set {
            this.cOUNTRYField = value;
        }
    }
    
    /// <remarks/>
    public string NOTE {
        get {
            return this.nOTEField;
        }
        set {
            this.nOTEField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace="", IsNullable=false)]
public partial class INDIVIDUAL_DOCUMENT {
    
    private string tYPE_OF_DOCUMENTField;
    
    private string tYPE_OF_DOCUMENT2Field;
    
    private string nUMBERField;
    
    private string iSSUING_COUNTRYField;
    
    private string dATE_OF_ISSUEField;
    
    private string cITY_OF_ISSUEField;
    
    private string cOUNTRY_OF_ISSUEField;
    
    private string nOTEField;
    
    /// <remarks/>
    public string TYPE_OF_DOCUMENT {
        get {
            return this.tYPE_OF_DOCUMENTField;
        }
        set {
            this.tYPE_OF_DOCUMENTField = value;
        }
    }
    
    /// <remarks/>
    public string TYPE_OF_DOCUMENT2 {
        get {
            return this.tYPE_OF_DOCUMENT2Field;
        }
        set {
            this.tYPE_OF_DOCUMENT2Field = value;
        }
    }
    
    /// <remarks/>
    public string NUMBER {
        get {
            return this.nUMBERField;
        }
        set {
            this.nUMBERField = value;
        }
    }
    
    /// <remarks/>
    public string ISSUING_COUNTRY {
        get {
            return this.iSSUING_COUNTRYField;
        }
        set {
            this.iSSUING_COUNTRYField = value;
        }
    }
    
    /// <remarks/>
    public string DATE_OF_ISSUE {
        get {
            return this.dATE_OF_ISSUEField;
        }
        set {
            this.dATE_OF_ISSUEField = value;
        }
    }
    
    /// <remarks/>
    public string CITY_OF_ISSUE {
        get {
            return this.cITY_OF_ISSUEField;
        }
        set {
            this.cITY_OF_ISSUEField = value;
        }
    }
    
    /// <remarks/>
    public string COUNTRY_OF_ISSUE {
        get {
            return this.cOUNTRY_OF_ISSUEField;
        }
        set {
            this.cOUNTRY_OF_ISSUEField = value;
        }
    }
    
    /// <remarks/>
    public string NOTE {
        get {
            return this.nOTEField;
        }
        set {
            this.nOTEField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace="", IsNullable=false)]
public partial class ENTITY {
    
    private string dATAIDField;
    
    private string vERSIONNUMField;
    
    private string fIRST_NAMEField;
    
    private string uN_LIST_TYPEField;
    
    private string rEFERENCE_NUMBERField;
    
    private System.DateTime lISTED_ONField;
    
    private System.DateTime sUBMITTED_ONField;
    
    private bool sUBMITTED_ONFieldSpecified;
    
    private string nAME_ORIGINAL_SCRIPTField;
    
    private string cOMMENTS1Field;
    
    private LIST_TYPE lIST_TYPEField;
    
    private string[] lAST_DAY_UPDATEDField;
    
    private ENTITY_ALIAS[] eNTITY_ALIASField;
    
    private ENTITY_ADDRESS[] eNTITY_ADDRESSField;
    
    private string sORT_KEYField;
    
    private string sORT_KEY_LAST_MODField;
    
    private System.DateTime dELISTED_ONField;
    
    private bool dELISTED_ONFieldSpecified;
    
    public ENTITY() {
        this.dATAIDField = "000000";
        this.vERSIONNUMField = "1";
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(DataType="integer")]
    public string DATAID {
        get {
            return this.dATAIDField;
        }
        set {
            this.dATAIDField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(DataType="integer")]
    public string VERSIONNUM {
        get {
            return this.vERSIONNUMField;
        }
        set {
            this.vERSIONNUMField = value;
        }
    }
    
    /// <remarks/>
    public string FIRST_NAME {
        get {
            return this.fIRST_NAMEField;
        }
        set {
            this.fIRST_NAMEField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(DataType="NCName")]
    public string UN_LIST_TYPE {
        get {
            return this.uN_LIST_TYPEField;
        }
        set {
            this.uN_LIST_TYPEField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(DataType="NCName")]
    public string REFERENCE_NUMBER {
        get {
            return this.rEFERENCE_NUMBERField;
        }
        set {
            this.rEFERENCE_NUMBERField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(DataType="date")]
    public System.DateTime LISTED_ON {
        get {
            return this.lISTED_ONField;
        }
        set {
            this.lISTED_ONField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(DataType="date")]
    public System.DateTime SUBMITTED_ON {
        get {
            return this.sUBMITTED_ONField;
        }
        set {
            this.sUBMITTED_ONField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool SUBMITTED_ONSpecified {
        get {
            return this.sUBMITTED_ONFieldSpecified;
        }
        set {
            this.sUBMITTED_ONFieldSpecified = value;
        }
    }
    
    /// <remarks/>
    public string NAME_ORIGINAL_SCRIPT {
        get {
            return this.nAME_ORIGINAL_SCRIPTField;
        }
        set {
            this.nAME_ORIGINAL_SCRIPTField = value;
        }
    }
    
    /// <remarks/>
    public string COMMENTS1 {
        get {
            return this.cOMMENTS1Field;
        }
        set {
            this.cOMMENTS1Field = value;
        }
    }
    
    /// <remarks/>
    public LIST_TYPE LIST_TYPE {
        get {
            return this.lIST_TYPEField;
        }
        set {
            this.lIST_TYPEField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("VALUE", IsNullable=false)]
    public string[] LAST_DAY_UPDATED {
        get {
            return this.lAST_DAY_UPDATEDField;
        }
        set {
            this.lAST_DAY_UPDATEDField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("ENTITY_ALIAS")]
    public ENTITY_ALIAS[] ENTITY_ALIAS {
        get {
            return this.eNTITY_ALIASField;
        }
        set {
            this.eNTITY_ALIASField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("ENTITY_ADDRESS")]
    public ENTITY_ADDRESS[] ENTITY_ADDRESS {
        get {
            return this.eNTITY_ADDRESSField;
        }
        set {
            this.eNTITY_ADDRESSField = value;
        }
    }
    
    /// <remarks/>
    public string SORT_KEY {
        get {
            return this.sORT_KEYField;
        }
        set {
            this.sORT_KEYField = value;
        }
    }
    
    /// <remarks/>
    public string SORT_KEY_LAST_MOD {
        get {
            return this.sORT_KEY_LAST_MODField;
        }
        set {
            this.sORT_KEY_LAST_MODField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(DataType="date")]
    public System.DateTime DELISTED_ON {
        get {
            return this.dELISTED_ONField;
        }
        set {
            this.dELISTED_ONField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool DELISTED_ONSpecified {
        get {
            return this.dELISTED_ONFieldSpecified;
        }
        set {
            this.dELISTED_ONFieldSpecified = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace="", IsNullable=false)]
public partial class ENTITY_ALIAS {
    
    private string qUALITYField;
    
    private string aLIAS_NAMEField;
    
    /// <remarks/>
    public string QUALITY {
        get {
            return this.qUALITYField;
        }
        set {
            this.qUALITYField = value;
        }
    }
    
    /// <remarks/>
    public string ALIAS_NAME {
        get {
            return this.aLIAS_NAMEField;
        }
        set {
            this.aLIAS_NAMEField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace="", IsNullable=false)]
public partial class ENTITY_ADDRESS {
    
    private string sTREETField;
    
    private string cITYField;
    
    private string sTATE_PROVINCEField;
    
    private string zIP_CODEField;
    
    private string cOUNTRYField;
    
    private string nOTEField;
    
    /// <remarks/>
    public string STREET {
        get {
            return this.sTREETField;
        }
        set {
            this.sTREETField = value;
        }
    }
    
    /// <remarks/>
    public string CITY {
        get {
            return this.cITYField;
        }
        set {
            this.cITYField = value;
        }
    }
    
    /// <remarks/>
    public string STATE_PROVINCE {
        get {
            return this.sTATE_PROVINCEField;
        }
        set {
            this.sTATE_PROVINCEField = value;
        }
    }
    
    /// <remarks/>
    public string ZIP_CODE {
        get {
            return this.zIP_CODEField;
        }
        set {
            this.zIP_CODEField = value;
        }
    }
    
    /// <remarks/>
    public string COUNTRY {
        get {
            return this.cOUNTRYField;
        }
        set {
            this.cOUNTRYField = value;
        }
    }
    
    /// <remarks/>
    public string NOTE {
        get {
            return this.nOTEField;
        }
        set {
            this.nOTEField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace="", IsNullable=false)]
public partial class INDIVIDUALS {
    
    private INDIVIDUAL[] iNDIVIDUALField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("INDIVIDUAL")]
    public INDIVIDUAL[] INDIVIDUAL {
        get {
            return this.iNDIVIDUALField;
        }
        set {
            this.iNDIVIDUALField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace="", IsNullable=false)]
public partial class TITLE {
    
    private string[] vALUEField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("VALUE")]
    public string[] VALUE {
        get {
            return this.vALUEField;
        }
        set {
            this.vALUEField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace="", IsNullable=false)]
public partial class DESIGNATION {
    
    private string[] vALUEField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("VALUE")]
    public string[] VALUE {
        get {
            return this.vALUEField;
        }
        set {
            this.vALUEField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace="", IsNullable=false)]
public partial class NATIONALITY {
    
    private string[] vALUEField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("VALUE")]
    public string[] VALUE {
        get {
            return this.vALUEField;
        }
        set {
            this.vALUEField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace="", IsNullable=false)]
public partial class ENTITIES {
    
    private ENTITY[] eNTITYField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("ENTITY")]
    public ENTITY[] ENTITY {
        get {
            return this.eNTITYField;
        }
        set {
            this.eNTITYField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace="", IsNullable=false)]
public partial class LAST_DAY_UPDATED {
    
    private string[] vALUEField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("VALUE")]
    public string[] VALUE {
        get {
            return this.vALUEField;
        }
        set {
            this.vALUEField = value;
        }
    }
}