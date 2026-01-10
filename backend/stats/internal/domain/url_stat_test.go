package domain

import (
	"testing"
	"time"
)

func TestAddAccess(t *testing.T) {
	accessessQuantity := 1
	lastAccess := time.Date(
		2025,
		time.January,
		1,
		0,
		0,
		0,
		0,
		time.UTC,
	)
	urlStat := UrlStat{
		Id:               "1234",
		OriginalUrl:      "http://test.com",
		AccessesQuantity: accessessQuantity,
		LastAccess:       lastAccess,
	}

	urlStat.AddAccess()

	if urlStat.AccessesQuantity != accessessQuantity+1 {
		t.Errorf("expected = %d; got = %d", accessessQuantity+1, urlStat.AccessesQuantity)
	}

	if urlStat.LastAccess.Before(lastAccess) || urlStat.LastAccess.Equal(lastAccess) {
		t.Errorf("expected LastAccesses to be greater than = %s", urlStat.LastAccess)
	}
}
