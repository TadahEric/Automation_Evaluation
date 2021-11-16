@shortlist
Feature: Shortlist

  Scenario: Trade can be added and removed from shortlist
     Given we set the destination to the homepage
      When  we load the member by 'RubyMineDIY'
      Then  I can add and remove from the shortlist

  Scenario: As a user I want to send a message to my saved trades
      Given I have saved test trades
      When  I try to send a message to all saved trades        
  	  Then  the message could be sent if required

