namespace CalculadoraImc;

/// <summary>Resultado completo de uma avaliação de IMC.</summary>
public record ResultadoImc(double Imc, string Classificacao);

/// <summary>
/// Calculadora de IMC (Índice de Massa Corporal) com classificação
/// de acordo com a tabela da ABESO.
/// </summary>
public class Calculadora
{
    public const double PesoMaximoKg = 500;
    public const double AlturaMaximaM = 3;

    /// <summary>Calcula o IMC (peso / altura²) arredondado em 2 casas decimais.</summary>
    /// <param name="pesoKg">Peso em quilogramas (maior que 0 e até 500).</param>
    /// <param name="alturaM">Altura em metros (maior que 0 e até 3).</param>
    /// <exception cref="ArgumentOutOfRangeException">Peso ou altura inválidos.</exception>
    public double CalcularImc(double pesoKg, double alturaM)
    {
        if (pesoKg <= 0 || pesoKg > PesoMaximoKg)
            throw new ArgumentOutOfRangeException(nameof(pesoKg), "Peso deve ser maior que 0 e no máximo 500 kg.");
        if (alturaM <= 0 || alturaM > AlturaMaximaM)
            throw new ArgumentOutOfRangeException(nameof(alturaM), "Altura deve ser maior que 0 e no máximo 3 m.");

        return Math.Round(pesoKg / (alturaM * alturaM), 2);
    }

    /// <summary>Classifica o IMC conforme a tabela da ABESO.</summary>
    /// <exception cref="ArgumentOutOfRangeException">IMC menor ou igual a zero.</exception>
    public string Classificar(double imc)
    {
        if (imc <= 0)
            throw new ArgumentOutOfRangeException(nameof(imc), "IMC deve ser maior que 0.");

        if (imc < 18.5) return "Magreza";
        if (imc < 25.0) return "Eutrofia";
        if (imc < 30.0) return "Sobrepeso";
        if (imc < 35.0) return "Obesidade grau I";
        if (imc < 40.0) return "Obesidade grau II";
        return "Obesidade grau III";
    }

    /// <summary>Calcula o IMC e já devolve a classificação correspondente.</summary>
    public ResultadoImc Avaliar(double pesoKg, double alturaM)
    {
        var imc = CalcularImc(pesoKg, alturaM);
        return new ResultadoImc(imc, Classificar(imc));
    }
}
