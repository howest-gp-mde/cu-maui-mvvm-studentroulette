﻿using Mde.Mvvm.StudentRoulette.Domain.Services;
using Mde.Mvvm.StudentRoulette.Domain.Services.Interfaces;
using Mde.Mvvm.StudentRoulette.Pages;
using Mde.Mvvm.StudentRoulette.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace Mde.Mvvm.StudentRoulette;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		builder.Services.AddTransient<StudentFormPage>();
		builder.Services.AddTransient<StudentFormViewModel>();
		
		builder.Services.AddTransient<StudentListPage>();
		builder.Services.AddTransient<StudentListViewModel>();

		builder.Services.AddTransient<IStudentService, JsonStudentService>();

		return builder.Build();
	}
}
