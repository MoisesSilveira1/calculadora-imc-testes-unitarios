using CalculadoraImc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CalculadoraImc.Tests.MSTest;

/// <summary>
/// Testes de unidade da calculadora de IMC com MSTest.
/// Convenção de nomes: Método_Cenário_ResultadoEsperado.
/// </summary>
[TestClass]
public class ImcTests
{
    private readonly Calculadora _calc = new();

    // ===== CT01 - CalcularImc =====

    [TestMethod]
    public void CalcularImc_PesoEAlturaValidos_RetornaImcArredondado()
        => Assert.AreEqual(22.86, _calc.CalcularImc(70, 1.75), 0.01);

    [TestMethod]
    public void CalcularImc_OutroPesoEAlturaValidos_RetornaImcCorreto()
        => Assert.AreEqual(19.53, _calc.CalcularImc(50, 1.60), 0.01);

    [TestMethod]
    public void CalcularImc_PesoZero_LancaExcecao()
        => Assert.ThrowsException<ArgumentOutOfRangeException>(() => _calc.CalcularImc(0, 1.75));

    [TestMethod]
    public void CalcularImc_PesoNegativo_LancaExcecao()
        => Assert.ThrowsException<ArgumentOutOfRangeException>(() => _calc.CalcularImc(-70, 1.75));

    [TestMethod]
    public void CalcularImc_AlturaZero_LancaExcecao()
        => Assert.ThrowsException<ArgumentOutOfRangeException>(() => _calc.CalcularImc(70, 0));

    [TestMethod]
    public void CalcularImc_AlturaNegativa_LancaExcecao()
        => Assert.ThrowsException<ArgumentOutOfRangeException>(() => _calc.CalcularImc(70, -1.75));

    [TestMethod]
    public void CalcularImc_AlturaEmCentimetros_LancaExcecao()
        => Assert.ThrowsException<ArgumentOutOfRangeException>(() => _calc.CalcularImc(70, 175));

    [TestMethod]
    public void CalcularImc_PesoAcimaDoLimite_LancaExcecao()
        => Assert.ThrowsException<ArgumentOutOfRangeException>(() => _calc.CalcularImc(501, 1.75));

    // ===== CT02 - Classificar (tabela ABESO e valores-limite) =====

    [TestMethod]
    public void Classificar_Imc22_86_RetornaEutrofia()
        => Assert.AreEqual("Eutrofia", _calc.Classificar(22.86));

    [TestMethod]
    public void Classificar_Imc18_4_RetornaMagreza()
        => Assert.AreEqual("Magreza", _calc.Classificar(18.4));

    [TestMethod]
    public void Classificar_Imc18_5_RetornaEutrofia()
        => Assert.AreEqual("Eutrofia", _calc.Classificar(18.5));

    [TestMethod]
    public void Classificar_Imc24_9_RetornaEutrofia()
        => Assert.AreEqual("Eutrofia", _calc.Classificar(24.9));

    [TestMethod]
    public void Classificar_Imc25_RetornaSobrepeso()
        => Assert.AreEqual("Sobrepeso", _calc.Classificar(25.0));

    [TestMethod]
    public void Classificar_Imc29_9_RetornaSobrepeso()
        => Assert.AreEqual("Sobrepeso", _calc.Classificar(29.9));

    [TestMethod]
    public void Classificar_Imc30_RetornaObesidadeGrauI()
        => Assert.AreEqual("Obesidade grau I", _calc.Classificar(30.0));

    [TestMethod]
    public void Classificar_Imc34_9_RetornaObesidadeGrauI()
        => Assert.AreEqual("Obesidade grau I", _calc.Classificar(34.9));

    [TestMethod]
    public void Classificar_Imc35_RetornaObesidadeGrauII()
        => Assert.AreEqual("Obesidade grau II", _calc.Classificar(35.0));

    [TestMethod]
    public void Classificar_Imc39_9_RetornaObesidadeGrauII()
        => Assert.AreEqual("Obesidade grau II", _calc.Classificar(39.9));

    [TestMethod]
    public void Classificar_Imc40_RetornaObesidadeGrauIII()
        => Assert.AreEqual("Obesidade grau III", _calc.Classificar(40.0));

    [TestMethod]
    public void Classificar_ImcZero_LancaExcecao()
        => Assert.ThrowsException<ArgumentOutOfRangeException>(() => _calc.Classificar(0));

    [TestMethod]
    public void Classificar_ImcNegativo_LancaExcecao()
        => Assert.ThrowsException<ArgumentOutOfRangeException>(() => _calc.Classificar(-5));

    // ===== CT03 - Avaliar =====

    [TestMethod]
    public void Avaliar_70kgE1_75m_RetornaImc22_86Eutrofia()
    {
        var r = _calc.Avaliar(70, 1.75);
        Assert.AreEqual(22.86, r.Imc, 0.01);
        Assert.AreEqual("Eutrofia", r.Classificacao);
    }

    [TestMethod]
    public void Avaliar_95kgE1_70m_RetornaObesidadeGrauI()
    {
        var r = _calc.Avaliar(95, 1.70);
        Assert.AreEqual(32.87, r.Imc, 0.01);
        Assert.AreEqual("Obesidade grau I", r.Classificacao);
    }

    [TestMethod]
    public void Avaliar_110kgE1_70m_RetornaObesidadeGrauII()
    {
        var r = _calc.Avaliar(110, 1.70);
        Assert.AreEqual(38.06, r.Imc, 0.01);
        Assert.AreEqual("Obesidade grau II", r.Classificacao);
    }

    [TestMethod]
    public void Avaliar_PesoZero_LancaExcecao()
        => Assert.ThrowsException<ArgumentOutOfRangeException>(() => _calc.Avaliar(0, 1.70));
}
