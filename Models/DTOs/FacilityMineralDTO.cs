namespace ExomineAPI.Models.DTOs;

public class FacilityMineralDTO
{
    public int Id { get; set; }
    public int FacilityId { get; set; }
    public int MineralId { get; set; }
    public int MineralQuantity { get; set; }

    public List<MineralDTO> Mineral { get; set; }

    public List<FacilityDTO> Facility { get; set; }
};