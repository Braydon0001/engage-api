namespace Engage.Domain.Enums
{
    public enum EmployeeTransactionTypeGroupId
    {
        //Allowances
        BackPay = 1,
        Overtime,
        Reimbursement,
        Unpaid,
        IncentiveRewards,
        ReimbursementAmount,
        SalaryAdvance,
        //Recurring Allowances
        TravelAllowanceRecurring,
        CellPhoneRecurring,
        //Deductions
        AssetSaleDeduction,
        BasicPayDeduction,
        CellPhoneDeduction,
        DamagedStockDeduction,
        VehicleDeduction,
        StaffDeduction,
        PersonalTravelDeduction,
        //Recurring Deductions
        AmountDeductionRecurring,
        LoanDeductionRecurring,
        CreditDeductionRecurring,
        //New Grouping
        //Allowances
        AmountAllowance,
        //Deductions
        AmountDeduction,
    }
}