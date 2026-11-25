using CalculadoraImc;
using Xunit;

namespace CalculadoraImc.Tests.XUnit;

/// <summary>
/// Desenho dos testes de unidade da calculadora de IMC (xUnit).
/// Convenção de nomes: Método_Cenário_ResultadoEsperado.
/// Cada método de teste corresponde a um fluxo dos casos de teste (CT01, CT02, CT03).
/// </summary>
public class ImcTests
{
    private readonly Calculadora _calc = new();

    // ===== CT01 - CalcularImc =====

    [Fact] // CT01 - Fluxo básico
    public void CalcularImc_PesoEAlturaValidos_RetornaImcArredondado()
    {
        Assert.Equal(22.86, _calc.CalcularImc(70, 1.75), 2);
    }

    [Fact] // CT01 - FA01
    public void CalcularImc_OutroPesoEAlturaValidos_RetornaImcCorreto()
    {
        Assert.Equal(19.53, _calc.CalcularImc(50, 1.60), 2);
    }

    [Fact] // CT01 - FA02
    public void CalcularImc_PesoZero_LancaExcecao()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => _calc.CalcularImc(0, 1.75));
    }

    [Fact] // CT01 - FA03
    public void CalcularImc_PesoNegativo_LancaExcecao()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => _calc.CalcularImc(-70, 1.75));
    }

    [Fact] // CT01 - FA04
    public void CalcularImc_AlturaZero_LancaExcecao()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => _calc.CalcularImc(70, 0));
    }

    [Fact] // CT01 - FA05
    public void CalcularImc_AlturaNegativa_LancaExcecao()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => _calc.CalcularImc(70, -1.75));
    }

    [Fact] // CT01 - FA06 (altura informada em centimetros por engano)
    public void CalcularImc_AlturaEmCentimetros_LancaExcecao()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => _calc.CalcularImc(70, 175));
    }

    [Fact] // CT01 - FA07
    public void CalcularImc_PesoAcimaDoLimite_LancaExcecao()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => _calc.CalcularImc(501, 1.75));
    }

    // ===== CT02 - Classificar (tabela ABESO e valores-limite) =====

    [Fact] // CT02 - Fluxo básico
    public void Classificar_Imc22_86_RetornaEutrofia()
    {
        Assert.Equal("Eutrofia", _calc.Classificar(22.86));
    }

    [Fact] // CT02 - FA01
    public void Classificar_Imc18_4_RetornaMagreza()
    {
        Assert.Equal("Magreza", _calc.Classificar(18.4));
    }

    [Fact] // CT02 - FA02
    public void Classificar_Imc18_5_RetornaEutrofia()
    {
        Assert.Equal("Eutrofia", _calc.Classificar(18.5));
    }

    [Fact] // CT02 - FA03
    public void Classificar_Imc24_9_RetornaEutrofia()
    {
        Assert.Equal("Eutrofia", _calc.Classificar(24.9));
    }

    [Fact] // CT02 - FA04
    public void Classificar_Imc25_RetornaSobrepeso()
    {
        Assert.Equal("Sobrepeso", _calc.Classificar(25.0));
    }

    [Fact] // CT02 - FA05
    public void Classificar_Imc29_9_RetornaSobrepeso()
    {
        Assert.Equal("Sobrepeso", _calc.Classificar(29.9));
    }

    [Fact] // CT02 - FA06
    public void Classificar_Imc30_RetornaObesidadeGrauI()
    {
        Assert.Equal("Obesidade grau I", _calc.Classificar(30.0));
    }

    [Fact] // CT02 - FA07
    public void Classificar_Imc34_9_RetornaObesidadeGrauI()
    {
        Assert.Equal("Obesidade grau I", _calc.Classificar(34.9));
    }

    [Fact] // CT02 - FA08
    public void Classificar_Imc35_RetornaObesidadeGrauII()
    {
        Assert.Equal("Obesidade grau II", _calc.Classificar(35.0));
    }

    [Fact] // CT02 - FA09
    public void Classificar_Imc39_9_RetornaObesidadeGrauII()
    {
        Assert.Equal("Obesidade grau II", _calc.Classificar(39.9));
    }

    [Fact] // CT02 - FA10
    public void Classificar_Imc40_RetornaObesidadeGrauIII()
    {
        Assert.Equal("Obesidade grau III", _calc.Classificar(40.0));
    }

    [Fact] // CT02 - FA11
    public void Classificar_ImcZero_LancaExcecao()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => _calc.Classificar(0));
    }

    [Fact] // CT02 - FA12
    public void Classificar_ImcNegativo_LancaExcecao()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => _calc.Classificar(-5));
    }

    // ===== CT03 - Avaliar (integração das duas unidades) =====

    [Fact] // CT03 - Fluxo básico
    public void Avaliar_70kgE1_75m_RetornaImc22_86Eutrofia()
    {
        var r = _calc.Avaliar(70, 1.75);
        Assert.Equal(22.86, r.Imc, 2);
        Assert.Equal("Eutrofia", r.Classificacao);
    }

    [Fact] // CT03 - FA01
    public void Avaliar_95kgE1_70m_RetornaObesidadeGrauI()
    {
        var r = _calc.Avaliar(95, 1.70);
        Assert.Equal(32.87, r.Imc, 2);
        Assert.Equal("Obesidade grau I", r.Classificacao);
    }

    [Fact] // CT03 - FA02
    public void Avaliar_110kgE1_70m_RetornaObesidadeGrauII()
    {
        var r = _calc.Avaliar(110, 1.70);
        Assert.Equal(38.06, r.Imc, 2);
        Assert.Equal("Obesidade grau II", r.Classificacao);
    }

    [Fact] // CT03 - FA03
    public void Avaliar_PesoZero_LancaExcecao()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => _calc.Avaliar(0, 1.70));
    }
}
