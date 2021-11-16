@search
Feature: Search
    Website visitors can search for trades and view their details

	Scenario: A website visitor can search for a valid trade by their name
		Given I have a valid trade 
		When I search for a trader by name on the home page
		Then I can see the correct details on the trade details page

	Scenario: A website visitor cannot search for trades with invalid details
		When I search for <tradeCategory> in <postcode> on the homepage
		Then I should see a <errorType> message indicating the search failure

		Examples: 
		| tradeCategory | postcode | errorType|
		| electrician   |          | postcode |
		| e423t4ertasd3 | PO6 2AB  | category |
		|               |          |          |

	Scenario Outline: A website visitor can Navigate through a recent search
	  When I search for <tradeCategory> in <postcode> on the homepage 
	  And I navigate through the recent search link within Recent Searches on the homepage
	  Then I should be directed to the search results page

	  Examples: 
	  | tradeCategory | postcode |
	  | Handyman      | PO9 3NW  |
	  | Plumber       | GL6 8HB  |
	
	Scenario:A website Visitor can only see three recent searches
	 When I search for four Trades in different locations	 
	 Then I should see three Recent Searched Trades within the Recent Searches element on the homepage
	
    Scenario Outline: A website visitor sort the results after searching
        When I search for Electrician in PO62AB on the homepage 
        And  I sort the results by <sortOrder>
        Then I see the results are sorted by <sortOrder>

		Examples: 
		| sortOrder     |
		| Nearest Me    |
		| Most Feedback |
		| Highest Rated |