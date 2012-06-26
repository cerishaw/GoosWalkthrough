Feature: Auction End to End

@mytag
Scenario: Single item - join, lose without bidding
	Given I have an Auction selling an item
	And The sniper has started to bid in that auction
	And The auction receives a join request
	When The auction announces that it is closed
	Then The sniper will show that it has lost the auction
