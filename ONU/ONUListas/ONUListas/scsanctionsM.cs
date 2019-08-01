using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ONUListas
{
    public class CONSOLIDATED_LISTM
    {
        [Key]
        public int Id { get; set; }
        public List<INDIVIDUALM> iNDIVIDUALS { get; set; }

        public List<ENTITYM> eNTITIES { get; set; }

        public System.DateTime dateGenerated { get; set; }
    }



    public partial class INDIVIDUALM
    {
        [Key]
        public int Id { get; set; }

        public string dATAIDField { get; set; }

        public string vERSIONNUMField { get; set; }

        public string fIRST_NAMEField { get; set; }

        public string sECOND_NAMEField { get; set; }

        public string tHIRD_NAMEField { get; set; }

        public string fOURTH_NAMEField { get; set; }

        public string uN_LIST_TYPEField { get; set; }

        public string rEFERENCE_NUMBERField { get; set; }

        public System.DateTime lISTED_ONField { get; set; }

        public string gENDERField { get; set; }

        public string sUBMITTED_BYField { get; set; }

        public string nAME_ORIGINAL_SCRIPTField { get; set; }

        public string cOMMENTS1Field { get; set; }

        public string nATIONALITY2Field { get; set; }

        public string tITLEField { get; set; }

        public string dESIGNATIONField { get; set; }

        public string nATIONALITYField { get; set; }

        public LIST_TYPEM lIST_TYPEField { get; set; }

        public string lAST_DAY_UPDATEDField { get; set; }

        public List<INDIVIDUAL_ALIASM> iNDIVIDUAL_ALIASField { get; set; }

        public List<INDIVIDUAL_ADDRESSM> iNDIVIDUAL_ADDRESSField { get; set; }

        public List<INDIVIDUAL_DATE_OF_BIRTHM> iNDIVIDUAL_DATE_OF_BIRTHField { get; set; }

        public List<INDIVIDUAL_PLACE_OF_BIRTHM> iNDIVIDUAL_PLACE_OF_BIRTHField { get; set; }

        public List<INDIVIDUAL_DOCUMENTM> iNDIVIDUAL_DOCUMENTField { get; set; }

        public string sORT_KEYField { get; set; }

        public string sORT_KEY_LAST_MODField { get; set; }

        public System.DateTime dELISTED_ONField { get; set; }

        public bool dELISTED_ONFieldSpecified { get; set; }
    }



    public partial class LIST_TYPEM
    {
        [Key]
        public int Id { get; set; }

        public string vALUEField { get; set; }
    }


    public partial class INDIVIDUAL_ALIASM
    {
        [Key]
        public int Id { get; set; }
        public string qUALITYField { get; set; }

        public string aLIAS_NAMEField { get; set; }

        public string dATE_OF_BIRTHField { get; set; }

        public string cITY_OF_BIRTHField { get; set; }

        public string cOUNTRY_OF_BIRTHField { get; set; }

        public string nOTEField { get; set; }
    }

    public partial class INDIVIDUAL_ADDRESSM
    {
        [Key]
        public int Id { get; set; }

        public string sTREETField { get; set; }

        public string cITYField { get; set; }

        public string sTATE_PROVINCEField { get; set; }

        public string zIP_CODEField { get; set; }

        public string cOUNTRYField { get; set; }

        public string nOTEField { get; set; }
    }


    public partial class INDIVIDUAL_DATE_OF_BIRTHM
    {
        [Key]
        public int Id { get; set; }
        public string tYPE_OF_DATEField { get; set; }

        public string nOTEField { get; set; }

        public object itemsField { get; set; }

        public string itemsElementNameField { get; set; }
    }

    public partial class INDIVIDUAL_PLACE_OF_BIRTHM
    {
        [Key]
        public int Id { get; set; }

        public string cITYField { get; set; }

        public string sTATE_PROVINCEField { get; set; }

        public string cOUNTRYField { get; set; }

        public string nOTEField { get; set; }
    }

        public partial class INDIVIDUAL_DOCUMENTM
    {
        [Key]
        public int Id { get; set; }

        public string tYPE_OF_DOCUMENTField { get; set; }

        public string tYPE_OF_DOCUMENT2Field { get; set; }

        public string nUMBERField { get; set; }

        public string iSSUING_COUNTRYField { get; set; }

        public string dATE_OF_ISSUEField { get; set; }

        public string cITY_OF_ISSUEField { get; set; }

        public string cOUNTRY_OF_ISSUEField { get; set; }

        public string nOTEField { get; set; }

    }

    public partial class ENTITYM
    {
        [Key]
        public int Id { get; set; }

        public string dATAIDField { get; set; }

        public string vERSIONNUMField { get; set; }

        public string fIRST_NAMEField { get; set; }

        public string uN_LIST_TYPEField { get; set; }

        public string rEFERENCE_NUMBERField { get; set; }

        public System.DateTime lISTED_ONField { get; set; }

        public System.DateTime sUBMITTED_ONField { get; set; }

        public bool sUBMITTED_ONFieldSpecified { get; set; }

        public string nAME_ORIGINAL_SCRIPTField { get; set; }

        public string cOMMENTS1Field { get; set; }

        public LIST_TYPEM lIST_TYPEField { get; set; }

        public string lAST_DAY_UPDATEDField { get; set; }

        public List<ENTITY_ALIASM> eNTITY_ALIASField { get; set; }

        public List<ENTITY_ADDRESSM> eNTITY_ADDRESSField { get; set; }

        public string sORT_KEYField { get; set; }

        public string sORT_KEY_LAST_MODField { get; set; }

        public System.DateTime dELISTED_ONField { get; set; }

        public bool dELISTED_ONFieldSpecified{ get; set; }

    }



    public partial class ENTITY_ALIASM
    {
        [Key]
        public int Id { get; set; }

        public string qUALITYField { get; set; }

        public string aLIAS_NAMEField { get; set; }
    }

    public partial class ENTITY_ADDRESSM
    {
        [Key]
        public int Id { get; set; }

        public string sTREETField { get; set; }

        public string cITYField { get; set; }

        public string sTATE_PROVINCEField { get; set; }

        public string zIP_CODEField { get; set; }

        public string cOUNTRYField { get; set; }

        public string nOTEField { get; set; }
    }


    public partial class INDIVIDUALSM
    {
        [Key]
        public int Id { get; set; }

        public List<INDIVIDUALM> iNDIVIDUALField { get; set; }
    }

    public partial class TITLEM
    {
        [Key]
        public int Id { get; set; }

        public string vALUEField { get; set; }
    }


    public partial class DESIGNATIONM
    {
        [Key]
        public int Id { get; set; }

        public string vALUEField { get; set; }
    }
    public partial class NATIONALITYM
    {
        [Key]
        public int Id { get; set; }

        public string vALUEField { get; set; }
    }


    public partial class ENTITIESM
    {
        [Key]
        public int Id { get; set; }

        public List<ENTITYM> eNTITYField { get; set; }

    }

    public partial class LAST_DAY_UPDATEDM
    {
        [Key]
        public int Id { get; set; }
        public string vALUEField { get; set; }

    }



    }
