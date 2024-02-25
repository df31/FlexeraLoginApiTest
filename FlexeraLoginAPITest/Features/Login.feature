Feature: Login

@login
Scenario Outline: Login with email and password
	Given user email is <email>
	And user password is <password>
	When post the credentials to login
	Then The loging result shows <status>
Examples:
	| email       | password | status       |
	|             |          | Bad Request  |
	| 123@123.com |          | Bad Request  |
	|             | 123      | Bad Request  |
	| 123@123.com | 123      | Unauthorized |
	