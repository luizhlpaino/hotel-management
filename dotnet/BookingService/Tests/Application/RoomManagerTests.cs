using Application.Room;
using Requests = Application.Requests.Room;
using Ports = Application.Ports.Room;
using DTO = Application.DTO.Room;
using Entities = Domain.Entities;
using Moq;
using Domain.Enums;

public class RoomManagerTests
{
    RoomManager roomManager;
    [SetUp]
    public void Setup() { }

    [Test]
    public async Task Should_Create_A_New_Room()
    {
        var roomDTO = new DTO.RoomDTO()
        {
            Name = "404",
            Level = 1,
            InMaintenance = false,
            Currency = AcceptedCurrencies.Dollar,
            Price = 250,
        };

        int expectedId = 111;

        var request = new Requests.CreateRoomRequest()
        {
            Data = roomDTO
        };

        var fakeRepo = new Mock<Ports.IRoomRepository>();
        fakeRepo.Setup(x => x.Create(It.IsAny<Entities.Room>()))
            .Returns(Task.FromResult(expectedId));
        roomManager = new RoomManager(fakeRepo.Object);

        var response = await roomManager.CreateRoom(request);

        Assert.IsNotNull(response);
        Assert.True(response.Success);
        Assert.AreEqual(expectedId, response.Data.Id);
    }
}