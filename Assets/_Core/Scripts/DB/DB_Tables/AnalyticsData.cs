using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SQLite4Unity3d;

public class AnalyticsData {

	[PrimaryKey]
	public int Id { get; set; }
	public string Uuid { get; set; }
	public string App { get; set; }
	public string Cohort_day { get; set; }
	public int Cohort_day_number { get; set; }
	public int Cohort_year_number { get; set; }
	public string Start_version { get; set; }
	public string Config_number { get; set; }
	public int Push_enabled { get; set; }
	public int Jailbroken { get; set; }
	public int Session { get; set; }
	public int Days_after_install { get; set; }
	public int Lives { get; set; }
	public int Levels_completed { get; set; }
	public int Games_started { get; set; }
	public int Total_time_played { get; set; }
	public float Money_spent { get; set; }
	public int Number_of_purchases { get; set; }
	public int Number_of_hard_purchases { get; set; }
	public int Total_hard_spent { get; set; }
	public int Total_hard_got { get; set; }
	public int Total_free_hard_got { get; set; }
	public int Total_paid_hard_got { get; set; }
	public int Current_free_hard { get; set; }
	public int Current_paid_hard { get; set; }
	public string Device_type { get; set; }
	public long Last_time_login { get; set; }
	public int Number_of_soft_purchases { get; set; }
	public int Total_soft_spent { get; set; }
	public int Total_soft_got { get; set; }
	public int Total_free_soft_got { get; set; }
	public int Total_paid_soft_got { get; set; }
	public int Current_free_soft { get; set; }
	public int Current_paid_soft { get; set; }
	public int Wins_in_a_row { get; set; }
	public int Losses_in_a_row { get; set; }
	public bool Payers { get; set; }

	public string ConversionData { get; set; }

	// Time and price of the last hard currency purchase
	public long Shop_last_puchase_time { get; set; }
	public float Shop_last_purchase_price_usd { get; set; }
	public bool First_spend_hard_after_gain { get; set;}
	public string Hard_boosters_state { get; set; }
}
