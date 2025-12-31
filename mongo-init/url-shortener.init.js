db = db.getSiblingDB("url-shortener");

db.createCollection("urls");
db.createCollection("redirectHost");

db.redirectHost.insertOne({
  Value: "http://localhost:5001",
  CreatedAt: new Date()
});
