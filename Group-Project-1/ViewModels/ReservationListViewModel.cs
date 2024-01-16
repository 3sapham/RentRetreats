using System;
using Group_Project_1.Models;
namespace Group_Project_1.ViewModels;

public class ReservationListViewModel
{
	public IEnumerable<Reservation> Reservations;
    public string? CurrentViewName;

    public ReservationListViewModel(IEnumerable<Reservation> reservations, string? currentViewName)
	{
		Reservations = reservations;
		CurrentViewName = currentViewName;
	}
}
