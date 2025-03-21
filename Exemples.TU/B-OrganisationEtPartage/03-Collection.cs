namespace Exemples.TU.B_OrganisationEtPartage;

// 1. Ressource partagée simple
public class SharedResource : IDisposable
{
    public int Value { get; set; }

    public SharedResource()
    {
        Console.WriteLine("SharedResource: Création");
        Value = 42;
    }

    public void Dispose()
    {
        Console.WriteLine("SharedResource: Libération");
    }
}

// 2. Fixture qui contient la ressource partagée
public class SharedResourceFixture_03 : IDisposable
{
    public SharedResource Resource { get; }

    public SharedResourceFixture_03()
    {
        Resource = new SharedResource();
    }

    public void Dispose()
    {
        Resource.Dispose();
    }
}

// 3. Définition de la collection
[CollectionDefinition("Shared Collection")]
public class SharedCollection : ICollectionFixture<SharedResourceFixture_03>
{
    // Classe vide, juste pour définir la collection
}

// 4. Premier groupe de tests utilisant la collection
[Collection("Shared Collection")]
public class TestGroupA
{
    private readonly SharedResourceFixture_03 _fixture;

    public TestGroupA(SharedResourceFixture_03 fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public void TestA1()
    {
        Console.WriteLine($"TestA1: Value = {_fixture.Resource.Value}");

        // Modifie la valeur pour démontrer l'état partagé
        _fixture.Resource.Value = 100;

        Assert.Equal(100, _fixture.Resource.Value);
    }

    [Fact]
    public void TestA2()
    {
        Console.WriteLine($"TestA2: Value = {_fixture.Resource.Value}");
        // On ne peut pas faire d'hypothèse sur la valeur exacte car l'ordre d'exécution n'est pas garanti
        // Mais on peut vérifier que la ressource existe
        Assert.NotNull(_fixture.Resource);
    }
}

// 5. Second groupe de tests utilisant la même collection
[Collection("Shared Collection")]
public class TestGroupB
{
    private readonly SharedResourceFixture_03 _fixture;

    public TestGroupB(SharedResourceFixture_03 fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public void TestB1()
    {
        Console.WriteLine($"TestB1: Value = {_fixture.Resource.Value}");
        // On ne peut pas faire d'hypothèse sur la valeur exacte car l'ordre d'exécution n'est pas garanti
        Assert.NotNull(_fixture.Resource);
    }
}


//NOTE : AssemblyFixture permet de n'exécuter du code qu'une fois par assembly lors des tests