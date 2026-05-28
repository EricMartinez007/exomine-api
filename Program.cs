using ExomineAPI.Models;
using ExomineAPI.Models.DTOs;

List<Mineral> minerals = new List<Mineral>
{
    new Mineral()
    {
      Id = 1,
      Name = "Iridium"
    },
    new Mineral()
    {
      Id = 2,
      Name = "Zircon"
    },
    new Mineral()
    {
      Id = 3,
      Name = "Sodium"
    },
    new Mineral()
    {
      Id = 4,
      Name = "Tungstite"
    },
    new Mineral()
    {
      Id = 5,
      Name = "Silicon"
    },
    new Mineral()
    {
      Id = 6,
      Name = "Gold"
    }
};

List<Facility> facilities = new List<Facility>
{
    new Facility()
    {
      Id = 1,
      Name = "Mars",
      IsActive = false
    },
    new Facility()
    {
      Id = 2,
      Name = "Jupiter",
      IsActive = true
    },
    new Facility()
    {
      Id = 3,
      Name = "Uranus",
      IsActive = true
    },
    new Facility()
    {
      Id = 4,
      Name = "Venus",
      IsActive = true
    }
};

List<FacilityMineral> facilityMinerals = new List<FacilityMineral>
{
    new FacilityMineral()
    {
      Id = 1,
      FacilityId = 4,
      MineralId = 1,
      MineralQuantity = 19
    },
    new FacilityMineral()
    {
      Id = 2,
      FacilityId = 2,
      MineralId = 3,
      MineralQuantity = 43
    },
    new FacilityMineral()
    {
      Id = 3,
      FacilityId = 1,
      MineralId = 5,
      MineralQuantity = 100
    },
    new FacilityMineral()
    {
      Id = 4,
      FacilityId = 3,
      MineralId = 2,
      MineralQuantity = 24
    },
    new FacilityMineral()
    {
      Id = 5,
      FacilityId = 4,
      MineralId = 2,
      MineralQuantity = 24
    },
    new FacilityMineral()
    {
      Id = 6,
      FacilityId = 2,
      MineralId = 5,
      MineralQuantity = 48
    },
    new FacilityMineral()
    {
      Id = 7,
      FacilityId = 3,
      MineralId = 6,
      MineralQuantity = 99
    },
    new FacilityMineral()
    {
      Id = 8,
      FacilityId = 4,
      MineralId = 5,
      MineralQuantity = 23
    },
    new FacilityMineral()
    {
      Id = 9,
      FacilityId = 3,
      MineralId = 3,
      MineralQuantity = 59
    },
    new FacilityMineral()
    {
      Id = 10,
      FacilityId = 2,
      MineralId = 4,
      MineralQuantity = 499
    },
    new FacilityMineral()
    {
      Id = 11,
      FacilityId = 4,
      MineralId = 6,
      MineralQuantity = 9
    }
};

List<Colony> colonies = new List<Colony>
{
    new Colony()
    {
      Id = 1,
      Name = "The Moon",
      Population = "40,000 People"
    },
    new Colony()
    {
      Id = 2,
      Name = "Pluto",
      Population = "2,000 People"
    },
    new Colony()
    {
      Id = 3,
      Name = "Saturn",
      Population = "600,000 People"
    }
};

List<Governor> governors = new List<Governor>
{
    new Governor()
    {
      Id = 1,
      Name = "Mary Doe",
      ColonyId = 3,
      IsActive = false
    },
    new Governor()
    {
      Id = 2,
      Name = "Marshall Smith",
      ColonyId = 3,
      IsActive = true
    },
    new Governor()
    {
      Id = 3,
      Name = "Paul Atriedes",
      ColonyId = 1,
      IsActive = true
    },
    new Governor()
    {
      Id = 4,
      Name = "Luanne James",
      ColonyId = 2,
      IsActive = true
    }
};

List<ColonyMineral> colonyMinerals = new List<ColonyMineral>
{
    new ColonyMineral()
    {
      Id = 1,
      ColonyId = 3,
      MineralId = 3,
      MineralQuantity = 3
    },
    new ColonyMineral()
    {
      Id = 2,
      ColonyId = 1,
      MineralId = 3,
      MineralQuantity = 1
    }
};



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// Add handlers below
app.MapGet("/governors", () =>
{
    return governors.Select(g => new GovernorDTO
    {
        Id = g.Id,
        Name = g.Name,
        ColonyId = g.ColonyId,
        IsActive = g.IsActive
    });
});

app.MapGet("/api/facilities", () =>
{
    return facilities.Select(f =>
    {
        return new FacilityDTO
        {
            Id = f.Id,
            Name = f.Name,
            IsActive = f.IsActive
        };
    });
});

app.MapGet("/api/facilities/{id}", (int id) =>
{
    Facility facility = facilities.FirstOrDefault(f => f.Id == id);

    if (facility == null)
    {
        return Results.NotFound();
    }

    return Results.Ok(new FacilityDTO
    {
        Id = facility.Id,
        Name = facility.Name,
        IsActive = facility.IsActive
    });
});

app.MapGet("/api/colonyMinerals", (int? colonyId, int? mineralId) =>
{
    List<ColonyMineral> result = colonyMinerals.ToList();

    if (colonyId != null)
    {
    result = result.Where(colonyMineral => colonyMineral.ColonyId == colonyId).ToList();
    }

    if (mineralId != null)
    {
    result = result.Where(colonyMineral => colonyMineral.MineralId == mineralId).ToList();
    }

    return result.Select(colonyMineral =>
    {
        return new ColonyMineralDTO
        {
            Id = colonyMineral.Id,
            ColonyId = colonyMineral.ColonyId,
            MineralId = colonyMineral.MineralId,
            MineralQuantity = colonyMineral.MineralQuantity,
            Mineral = new MineralDTO
            {
                Id = minerals.First(m => m.Id == colonyMineral.MineralId).Id,
                Name = minerals.First(m => m.Id == colonyMineral.MineralId).Name
            },
            Colony = new ColonyDTO
            {
                Id = colonies.First(c => c.Id == colonyMineral.ColonyId).Id,
                Name = colonies.First(c => c.Id == colonyMineral.ColonyId).Name,
                Population = colonies.First(c => c.Id == colonyMineral.ColonyId).Population
            }
        };
    });
}); 

app.Run();
