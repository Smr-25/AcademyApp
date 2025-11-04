// See https://aka.ms/new-console-template for more information

using AcademyApp.BLL.Interfaces;
using AcademyApp.BLL.Sevices;
using AcademyApp.DLL.Data;
using Microsoft.Extensions.DependencyInjection;

var serviceCollection = new ServiceCollection();
serviceCollection.AddScoped<AcademyDbContext>();
serviceCollection.AddScoped<IGroupService, GroupService>();
var serviceProvider = serviceCollection.BuildServiceProvider();
var groupService = serviceProvider.GetRequiredService<IGroupService>();
// foreach (var group in groupService.GetAllGroups())
//     Console.WriteLine(group);
var result = await groupService.GetGroupByIdAsync(2);
Console.WriteLine(result);