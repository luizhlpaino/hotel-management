using DTO = Application.Guest.DTO;
using Requests = Application.Guest.Requests;
using Responses = Application.Responses;
using Entities = Domain.Entities;
using Domain.Ports;
using Moq;

namespace Application;

public class Tests
{
    GuestManager guestManager;
    [SetUp]
    public void Setup() { }

    [Test]
    public async Task Should_Create_A_New_Guest()
    {
        var guestDTO = new DTO.GuestDTO()
        {
            Name = "Albert",
            Surname = "Einstein",
            Email = "ainstein@gmail.com",
            IdNumber = "1234",
            IdTypeCode = 1,
        };

        int expectedId = 222;

        var request = new Requests.CreateGuestRequest()
        {
            Data = guestDTO
        };

        var fakeRepo = new Mock<IGuestRepository>();
        fakeRepo.Setup(x => x.Create(It.IsAny<Entities.Guest>())).Returns(Task.FromResult(expectedId));
        guestManager = new GuestManager(fakeRepo.Object);

        var response = await guestManager.CreateGuest(request);
        Assert.IsNotNull(response);
        Assert.True(response.Success);
        Assert.AreEqual(expectedId, response.Data.Id);
        Assert.AreEqual(guestDTO.Name, response.Data.Name);
    }

    [TestCase("")]
    [TestCase(null)]
    [TestCase("a")]
    [TestCase("ab")]
    [TestCase("abc")]
    public async Task Should_Return_A_InvalidPersonDocumentIdException_When_Docs_Are_Invalid(string documentIdNumber)
    {
        var guestDTO = new DTO.GuestDTO()
        {
            Name = "Albert",
            Surname = "Einstein",
            Email = "ainstein@gmail.com",
            IdNumber = documentIdNumber,
            IdTypeCode = 1,
        };

        var request = new Requests.CreateGuestRequest()
        {
            Data = guestDTO
        };

        var fakeRepo = new Mock<IGuestRepository>();
        fakeRepo.Setup(x => x.Create(It.IsAny<Entities.Guest>())).Returns(Task.FromResult(222));
        guestManager = new GuestManager(fakeRepo.Object);

        var response = await guestManager.CreateGuest(request);
        Assert.IsNotNull(response);
        Assert.False(response.Success);
        Assert.AreEqual(response.ErrorCode, Responses.ErrorCodes.INVALID_PERSON_ID);
    }

    [TestCase("", "Surname", "new@email.com")]
    [TestCase("Name", "", "new@email.com")]
    [TestCase("Name", "Surname", "")]
    [TestCase(null, "Surname", "new@email.com")]
    [TestCase("Name", null, "new@email.com")]
    [TestCase("Name", "Surname", null)]
    public async Task Should_Return_A_MissingRequiredInformationException_When_Required_Fields_Are_Invalid(string name, string surname, string email)
    {
        var guestDTO = new DTO.GuestDTO()
        {
            Name = name,
            Surname = surname,
            Email = email,
            IdNumber = "1234",
            IdTypeCode = 1,
        };

        var request = new Requests.CreateGuestRequest()
        {
            Data = guestDTO
        };

        var fakeRepo = new Mock<IGuestRepository>();
        fakeRepo.Setup(x => x.Create(It.IsAny<Entities.Guest>())).Returns(Task.FromResult(222));
        guestManager = new GuestManager(fakeRepo.Object);

        var response = await guestManager.CreateGuest(request);
        Assert.IsNotNull(response);
        Assert.False(response.Success);
        Assert.AreEqual(response.ErrorCode, Responses.ErrorCodes.MISSING_REQUIRED_INFORMATION);
    }

    [TestCase("email.com")]
    [TestCase("email@emailcom")]
    public async Task Should_Return_A_InvalidEmailException_When_Email_Is_Invalid(string email)
    {
        var guestDTO = new DTO.GuestDTO()
        {
            Name = "Albert",
            Surname = "Einstein",
            Email = email,
            IdNumber = "1234",
            IdTypeCode = 1,
        };

        var request = new Requests.CreateGuestRequest()
        {
            Data = guestDTO
        };

        var fakeRepo = new Mock<IGuestRepository>();
        fakeRepo.Setup(x => x.Create(It.IsAny<Entities.Guest>())).Returns(Task.FromResult(222));
        guestManager = new GuestManager(fakeRepo.Object);

        var response = await guestManager.CreateGuest(request);
        Assert.IsNotNull(response);
        Assert.False(response.Success);
        Assert.AreEqual(Responses.ErrorCodes.INVALID_EMAIL, response.ErrorCode);
    }

    [Test]
    public async Task Should_Return_GuestNotFoundException_When_Guest_Is_Not_Found_With_Id()
    {
        var fakeRepo = new Mock<IGuestRepository>();
        fakeRepo.Setup(x => x.Get(333)).Returns(Task.FromResult<Entities.Guest?>(null));
        guestManager = new GuestManager(fakeRepo.Object);
        var response = await guestManager.GetGuest(333);

        Assert.IsNotNull(response);
        Assert.False(response.Success);
        Assert.AreEqual(Responses.ErrorCodes.GUEST_NOT_FOUND, response.ErrorCode);
    }

    [Test]
    public async Task Should_Return_A_Guest_With_Success()
    {
        var fakeRepo = new Mock<IGuestRepository>();
        var fakeGuest = new Entities.Guest
        {
            Id = 333,
            Name = "Albert",
            Surname = "Einstein",
            Email = "ainstein@gmail.com",
            DocumentId = new Domain.ValueObjects.PersonId
            {
                DocumentType = Domain.Enums.DocumentType.DriverLicense,
                IdNumber = "1234"
            }
        };

        fakeRepo.Setup(x => x.Get(333)).Returns(Task.FromResult((Entities.Guest?)fakeGuest));
        guestManager = new GuestManager(fakeRepo.Object);
        var response = await guestManager.GetGuest(333);

        Assert.IsNotNull(response);
        Assert.True(response.Success);
        Assert.AreEqual(333, response.Data.Id);
        Assert.AreEqual("Albert", response.Data.Name);
    }


}