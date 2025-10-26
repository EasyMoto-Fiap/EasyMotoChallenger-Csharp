using Microsoft.ML;

namespace EasyMoto.Api.ML;

public interface IMaintenancePredictionService
{
    PredictMaintenanceResponse Predict(PredictMaintenanceRequest request);
}

public sealed class MaintenancePredictionService : IMaintenancePredictionService
{
    readonly MLContext _ml;
    readonly ITransformer _model;

    public MaintenancePredictionService()
    {
        _ml = new MLContext(seed: 42);
        var training = GetTrainingData().ToList();
        var data = _ml.Data.LoadFromEnumerable(training);

        var pipeline = _ml.Transforms.Concatenate(
                "Features",
                nameof(MaintenanceData.Age),
                nameof(MaintenanceData.Categoria),
                nameof(MaintenanceData.Status),
                nameof(MaintenanceData.Ativo),
                nameof(MaintenanceData.Meses),
                nameof(MaintenanceData.Km))
            .Append(_ml.BinaryClassification.Trainers.SdcaLogisticRegression());

        _model = pipeline.Fit(data);
    }

    public PredictMaintenanceResponse Predict(PredictMaintenanceRequest req)
    {
        var year = DateTime.UtcNow.Year;
        var input = new MaintenanceData
        {
            Age = Math.Max(0, year - req.Ano),
            Categoria = req.Categoria,
            Status = req.StatusOperacional,
            Ativo = req.Ativo ? 1f : 0f,
            Meses = req.MesesDesdeUltimaRevisao,
            Km = req.KmDesdeUltimaRevisao
        };

        var engine = _ml.Model.CreatePredictionEngine<MaintenanceData, MaintenancePrediction>(_model);
        var pred = engine.Predict(input);

        return new PredictMaintenanceResponse
        {
            ManutencaoRecomendada = pred.PredictedLabel,
            Probabilidade = pred.Probability
        };
    }

    static IEnumerable<MaintenanceData> GetTrainingData()
    {
        yield return new MaintenanceData { Age = 1, Categoria = 0, Status = 0, Ativo = 1, Meses = 3,  Km = 1500, Label = false };
        yield return new MaintenanceData { Age = 2, Categoria = 0, Status = 0, Ativo = 1, Meses = 6,  Km = 4000, Label = true  };
        yield return new MaintenanceData { Age = 3, Categoria = 1, Status = 1, Ativo = 1, Meses = 2,  Km = 800,  Label = false };
        yield return new MaintenanceData { Age = 5, Categoria = 1, Status = 1, Ativo = 1, Meses = 9,  Km = 7000, Label = true  };
        yield return new MaintenanceData { Age = 6, Categoria = 0, Status = 2, Ativo = 1, Meses = 12, Km = 9000, Label = true  };
        yield return new MaintenanceData { Age = 4, Categoria = 0, Status = 0, Ativo = 1, Meses = 1,  Km = 300,  Label = false };
        yield return new MaintenanceData { Age = 7, Categoria = 1, Status = 2, Ativo = 1, Meses = 10, Km = 8500, Label = true  };
        yield return new MaintenanceData { Age = 8, Categoria = 0, Status = 1, Ativo = 0, Meses = 4,  Km = 2500, Label = true  };
        yield return new MaintenanceData { Age = 2, Categoria = 1, Status = 0, Ativo = 1, Meses = 5,  Km = 3500, Label = true  };
        yield return new MaintenanceData { Age = 1, Categoria = 0, Status = 0, Ativo = 1, Meses = 2,  Km = 500,  Label = false };
    }
}

public sealed class PredictMaintenanceRequest
{
    public int Ano { get; set; }
    public int Categoria { get; set; }
    public int StatusOperacional { get; set; }
    public bool Ativo { get; set; }
    public int MesesDesdeUltimaRevisao { get; set; }
    public int KmDesdeUltimaRevisao { get; set; }
}

public sealed class PredictMaintenanceResponse
{
    public bool ManutencaoRecomendada { get; set; }
    public float Probabilidade { get; set; }
}

public sealed class MaintenanceData
{
    public float Age { get; set; }
    public float Categoria { get; set; }
    public float Status { get; set; }
    public float Ativo { get; set; }
    public float Meses { get; set; }
    public float Km { get; set; }
    public bool Label { get; set; }
}

public sealed class MaintenancePrediction
{
    public bool PredictedLabel { get; set; }
    public float Probability { get; set; }
    public float Score { get; set; }
}
