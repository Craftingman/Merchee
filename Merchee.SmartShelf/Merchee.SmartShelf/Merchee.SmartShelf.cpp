#include <cpprest/http_client.h>
#include <cpprest/filestream.h>
#include <cpprest/json.h>
#include <iostream>

using namespace std;

const string SERVER_ADDRESS{ "http://localhost:5031/" };
const string DEVICE_BARCODE{ "0000000000006" };

const string GET_SHELF_PRODUCT_URL{ "shelfProducts/byShelf" };
const string ADD_CUSTOMER_SHELF_ACTION_URL{ "customerShelfActions" }; 

// Example model class representing a product
class ShelfProduct {
public:
    std::string id;
    double fullWeight;

    // Deserialize JSON data into the model
    static ShelfProduct FromJson(const web::json::value& jsonData) {
        ShelfProduct product;
        product.id = utility::conversions::to_utf8string(jsonData.at(U("id")).as_string());
        product.fullWeight = jsonData.at(U("fullWeight")).as_double();
        return product;
    }
};

class CustomerShelfAction {
public:
    std::string shelfProductId;
    int type;
    int quantity;

    web::json::value ToJson() const {
        web::json::value jsonData;
        jsonData[U("shelfProductId")] = web::json::value::string(utility::conversions::to_string_t(shelfProductId));
        jsonData[U("type")] = web::json::value::number(type);
        jsonData[U("quantity")] = web::json::value::number(quantity);
        return jsonData;
    }
};

ShelfProduct getShelfProduct(std::string accessToken) {
    ShelfProduct result;
    try {
        std::string url = SERVER_ADDRESS + GET_SHELF_PRODUCT_URL;
        // Create a URI object from the base URL
        web::uri_builder builder(utility::conversions::to_string_t(url));

        // Query parameters
        std::map<std::string, std::string> queryParams;
        queryParams["shelfBarcode"] = DEVICE_BARCODE;

        // Append the query parameters to the URI
        for (const auto& kvp : queryParams) {
            builder.append_query(utility::conversions::to_string_t(kvp.first), utility::conversions::to_string_t(kvp.second));
        }

        // Create an HTTP client with the modified URI
        web::http::client::http_client client(builder.to_uri());

        // Create an HTTP request with the modified URI
        web::http::http_request request(web::http::methods::GET);

        // Set the headers
        request.headers().add(U("Access-Token"), utility::conversions::to_string_t(accessToken));

        // Send the HTTP GET request
        web::http::http_response response = client.request(request).get();

        // Check if the request was successful (HTTP status code 200)
        if (response.status_code() == web::http::status_codes::OK) {
            // Read the response body as a string
            std::string responseBody = response.extract_utf8string().get();

            // Parse the response JSON into models
            web::json::value jsonResponse = web::json::value::parse(utility::conversions::to_string_t(responseBody));
            result = ShelfProduct::FromJson(jsonResponse);
        }
        else {
            std::cout << "Error: " << response.status_code() << std::endl;
        }
    }
    catch (const std::exception& e) {
        std::cout << "Error: " << e.what() << std::endl;
    }

    return result;
}

ShelfProduct sendCustomerShelfAction(CustomerShelfAction* shelfAction, std::string accessToken) {
    ShelfProduct result;
    try {
        std::string url = SERVER_ADDRESS + ADD_CUSTOMER_SHELF_ACTION_URL;
        // Create a URI object from the base URL
        web::uri_builder builder(utility::conversions::to_string_t(url));

        // Create an HTTP client with the modified URI
        web::http::client::http_client client(builder.to_uri());

        // Create an HTTP request with the modified URI
        web::http::http_request request(web::http::methods::POST);

        // Set the headers
        request.headers().add(U("Access-Token"), utility::conversions::to_string_t(accessToken));

        request.set_body(shelfAction->ToJson());

        // Send the HTTP GET request
        web::http::http_response response = client.request(request).get();

        // Check if the request was successful (HTTP status code 200)
        if (response.status_code() == web::http::status_codes::OK) {
            std::cout << "Success: " << response.status_code() << std::endl;
        }
        else {
            std::cout << "Error: " << response.status_code() << std::endl;
        }
    }
    catch (const std::exception& e) {
        std::cout << "Error: " << e.what() << std::endl;
    }

    return result;
}

std::string readAccessToken() {
    return "nYoBmBT4heYmiO2ozyqLwEqHWtVqHtbOPF30QoWh+ec=";
}

std::string accessToken = { "" };
double currentWeight = 100;
ShelfProduct shelfProduct;

int getChangedCount(double currentWeight, double newWeight) {
    return round(((newWeight - currentWeight) / shelfProduct.fullWeight));
}

int main() {
    if (accessToken == "") {
        accessToken = readAccessToken();
    }

    shelfProduct = getShelfProduct(accessToken);

    while (true)
    {
        double newWeight = 0;

        std::cout << "Current weight: " << currentWeight << std::endl;
        std::cout << "Enter new weight: " << std::endl;
        std::cin >> newWeight;

        double newWeightRounded = round(newWeight * 10000.0) / 10000.0;
        double currentWeightRounded = round(currentWeight * 10000.0) / 10000.0;
        if (newWeightRounded == currentWeightRounded) {
            continue;
        }

        CustomerShelfAction* shelfAction = new CustomerShelfAction();
        shelfAction->shelfProductId = shelfProduct.id;

        int changedCount = getChangedCount(currentWeightRounded, newWeightRounded);
        if (changedCount == 0) {
            continue;
        }

        if (changedCount < 0) {
            shelfAction->quantity = -1 * changedCount;
            shelfAction->type = 0;
        }
        else {
            shelfAction->quantity = changedCount;
            shelfAction->type = 1;
        }

        sendCustomerShelfAction(shelfAction, accessToken);

        currentWeight = newWeightRounded;
    }

    return 0;
}