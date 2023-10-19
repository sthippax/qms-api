using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QmsDAL.Models
{
    public class PreSilicon
    {
        public List<PlaformSkuList> platformSku { get; set; }
        public List<PreSiliconList> preSilicon { get; set; }
    }
    public class PostSiliconDefects
    {
        public List<Platform> platform { get; set; }
        public List<PostSiliconDefectsList> postSiliconDefects { get; set; }
    }
    public class Platform
    {
        public int PlatformId { get; set; }
        public string PlatformName { get; set; }
        public string PlatformShortName { get; set; }
    }
    public class PostSiliconDeviation
    {
        public List<PlaformSkuList> platformSku { get; set; }
        public List<PostSiliconDeviationList> postSiliconDeviation { get; set; }
    }
    public class PlaformSkuList
    {
        public bool IsActive { get; set; } = false;
        public int PlatformId { get; set; }
        public string PlatformName { get; set; }
        public string PlatformShortName { get; set; }
        public List<SkuList> Sku { get; set; }               
    }
    public class SkuList
    {
        public string SkuName { get; set; }
        public int SkuId { get; set; }
        public bool isActive { get; set; } = false;
    }
    public class PreSiliconList
    {
        public string PlatformName { get; set; }
        public string PlatformShortName { get; set; }
        public string SkuName { get; set; }
        public double fRNewPercentage { get; set; }
        public double fRLegacyPercentage { get; set; }
        public int fRNewCounts { get; set; }
        public int fRLegacyCounts { get; set; }
        public double CurrentNewPercentage { get; set; }
        public double CurrentLegacyPercentage { get; set; }
        public int CurrentNewCounts { get; set; }
        public int CurrentLegacyCounts { get; set; }
        public int ccBTotal { get; set; }
        public int ccBAdded { get; set; }
        public int ccBRemoved { get; set; }
        public double FR_Simulation_Snapshot { get; set; }
        public double FR_Emulation_Snapshot { get; set; }
        public double N_1_Snapshot { get; set; }
        public double EnabledIntegrated_Snapshot { get; set; }
        public double PRQPV_PreSilicon_Snapshot { get; set; }
        public double PRQPV_PostSilicon_Snapshot { get; set; }
        public double FR_Simulation_Current { get; set; }
        public double FR_Emulation_Current { get; set; }
        public double N_1_Current { get; set; }
        public double EnabledIntegrated_Current { get; set; }
        public double PRQPV_PreSilicon_Current { get; set; }
        public double PRQPV_PostSilicon_Current { get; set; }
        
    }
    public class GetPlatformSku
    {
        public int PlatformId { get; set; }
        public string PlatformName { get; set; }
        public string PlatformShortName { get; set; }
        public int SkuId { get; set; }
        public string SkuName { get; set; }
    }
    public class PostSiliconDefectsList
    {
        public string PlatformName { get; set; }
        public string PlatformShortName { get; set; }
        public string SkuName { get; set; }        
        public int CurrentDefects { get; set; }
        public int CurrentTPT { get; set; }
        public int Defects { get; set; }
        public int TPT { get; set; }
    }
    public class PostSiliconDeviationList
    {
        public string PlatformName { get; set; }
        public string PlatformShortName { get; set; }
        public string SkuName { get; set; }
        public int DPMO { get; set; }
        public int DPMT { get; set; }
        public string KPIPower { get; set; }
        public string KPIPerformance { get; set; }
        public int BKCPercentage { get; set; }        
        public string unique_defects_current { get; set; }
        public string unique_defects_presilicon { get; set; }
    }
}
