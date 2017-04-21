﻿using CreditCardIocSample.Client;
using CreditCardIocSample.Model;

namespace CreditCardIocSample
{
	public class MasterCard
	{
		private readonly CreditCardClient _creditCardClient;

		public MasterCard()
		{
			_creditCardClient = new CreditCardClient();
		}

		public CustomerCardResult ChargeCard(CreditCard creditCard)
		{
			var result = _creditCardClient.SendRequest(creditCard);

			var masterCardResult = new CustomerCardResult();
			masterCardResult.CardHolderName = creditCard.CardHolderFullName;

			if (result != null)
			{
				masterCardResult.AuthCode = result.AuthorizationCode;
				masterCardResult.PaymentStatus = PaymentStatus.Approved;
				masterCardResult.ErrorMessage = string.Empty;
				masterCardResult.MaskedCardNumber = creditCard.CardNumber.Substring(11);
				masterCardResult.Token = $"{creditCard.CardHolderFullName}|{result.AuthorizationCode}|{result.TransactionId}";
			}
			else
			{
				masterCardResult.ErrorMessage = "Some Error";
				masterCardResult.PaymentStatus = PaymentStatus.Error;
				masterCardResult.Token = string.Empty;
				masterCardResult.TranscationId = string.Empty;
				masterCardResult.MaskedCardNumber = "MASTERXXXX";
			}

			return masterCardResult;
		}
	}
}